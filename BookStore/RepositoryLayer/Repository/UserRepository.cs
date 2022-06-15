using Microsoft.Extensions.Configuration;
using ModelLayer;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
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
    }
}
