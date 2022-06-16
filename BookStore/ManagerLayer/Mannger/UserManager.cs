using ManagerLayer.Interface;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Mannger
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repo;
        public UserManager(IUserRepository repo)
        {
            this.repo = repo;
        }

        public async Task<RegisterModel> Register(RegisterModel register)
        {
            try
            {
                return await this.repo.Register(register);
            }
            catch (Exception e)
            {

                throw new Exception (e.Message);
            }
        }

        public async Task<RegisterModel> Login(LoginModel login)
        {
            try
            {
                return await this.repo.Login(login);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
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
                return await this.repo.Reset(reset);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        
    }

}
