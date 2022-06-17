using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        Task<RegisterModel> Register(RegisterModel register);
        Task<RegisterModel> Login(LoginModel login);
        Task<RegisterModel> Reset(ResetModel reset);
        Task<bool> Forgot(string emailID);
        //public string GetJWTToken(string emailID);

    }
}
