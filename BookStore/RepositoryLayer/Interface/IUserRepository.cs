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
    }
}
