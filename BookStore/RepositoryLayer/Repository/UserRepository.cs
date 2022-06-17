using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace RepositoryLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<RegisterModel> User;
        private readonly IConfiguration configuration;

        public UserRepository(IDBSetting db, IConfiguration configuration)
        {

            this.configuration = configuration;
            var userclient = new MongoClient(db.ConnectionString);
            var database = userclient.GetDatabase(db.DatabaseName);
            User = database.GetCollection<RegisterModel>("User");
        }
        public async Task<RegisterModel> Register(RegisterModel register)
        {
            try
            {
                var check = await this.User.AsQueryable().Where(x => x.emailID == register.emailID).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this.User.InsertOneAsync(register);
                    return register;
                }
                return null;
            }
            catch (ArgumentNullException e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<RegisterModel> Login(LoginModel login)
        {
            try
            {
                var check = await this.User.AsQueryable().Where(x => x.emailID == login.emailID).FirstOrDefaultAsync();
                if (check != null)
                {
                    check = await this.User.AsQueryable().Where(x => x.password == login.password).FirstOrDefaultAsync();
                    if (check != null)
                    {
                        return check;
                    }
                    return null;
                }
                return null;
            }
            catch (ArgumentNullException e)
            {

                throw new Exception(e.Message);
            }
        }

        
        public async Task<bool> Forgot(string emailID)
        {
            try
            {
                var check = await this.User.AsQueryable().Where(x => x.emailID == emailID).FirstOrDefaultAsync();
                if (check == null)
                {
                    return false;
                }
                

                MessageQueue queue;
                //Add message to queue
                if (MessageQueue.Exists(@".\Private$\BooKStore"))
                {
                    queue = new MessageQueue(@".\Private$\BooKStore");
                }

                else
                {
                    queue = MessageQueue.Create(@".\Private$\BooKStore");
                }

                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = GetJWTToken(emailID);
                message.Label = "Forgot password Email";
                queue.Send(message);

                Message msg = queue.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailServices.SendMail(emailID, message.Body.ToString());
                queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                queue.BeginReceive();
                queue.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailServices.SendMail(e.Message.ToString(), GetJWTToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
            }
        }
        public string GetJWTToken(string emailID)
        {
            if (emailID == null)
            {
                return null;
            }

            // generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("emailID", emailID),

                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =
                               new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<RegisterModel> Reset(ResetModel reset)
        {
            
            try
            {
                var check = await this.User.AsQueryable().Where(x => x.emailID == reset.emailID).FirstOrDefaultAsync();
                if (check != null)
                {
                    await this.User.UpdateOneAsync(x => x.emailID == reset.emailID,
                        Builders<RegisterModel>.Update.Set(x => x.password , reset.ConfirmPassword));
                    return check;
                }
                return null;
            }
            catch (ArgumentNullException e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
