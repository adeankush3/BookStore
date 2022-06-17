using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using ModelLayer;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
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
                if (check != null)
                {
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = true;
                    client.Credentials = new NetworkCredential("shaluade67@gmail.com", "Shalu@123");
                    MailMessage msgObj = new MailMessage();
                    msgObj.To.Add(emailID);
                    msgObj.From = new MailAddress("shaluade67@gmail.com");
                    msgObj.Subject = "Password Reset Link";
                    //msgObj.Body = $"www.bookstore.com/reset-password/{token}";
                    client.Send(msgObj);
                }
                return false;
               
                //MessageQueue queue;
                ////Add message to queue
                //if (MessageQueue.Exists(@".\Private$\BooKStore"))
                //{
                //    queue = new MessageQueue(@".\Private$\BooKStore");
                //}

                //else
                //{
                //    queue = MessageQueue.Create(@".\Private$\BooKStore");
                //}

                //Message message = new Message();
                //message.Formatter = new BinaryMessageFormatter();
                ////message.Body = GetJWTToken(emailID, User);
                //message.Label = "Forgot password Email";
                //queue.Send(message);

                //Message msg = queue.Receive();
                //msg.Formatter = new BinaryMessageFormatter();
                //EmailServices.SendMail(emailID, message.Body.ToString());
                //queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                //queue.BeginReceive();
                //queue.Close();
                //return true;
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
                //EmailServices.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
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
