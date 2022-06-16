//using Couchbase.Management.Users;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        //private readonly ILogger<UserController> logger;

        public UserController(IUserManager manager) //, ILogger<UserController> logger
        {
            this.manager = manager;
            //this.logger = logger;
        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            try
            {
                //this.logger.LogInformation(register.fullName + "Is Trying To Register");
                var resp = await this.manager.Register(register);
                if (resp != null)
                {
                    //this.logger.LogInformation(register.fullName + "Register Successfully");
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "User Register Successfully", Data = resp });
                }
                else
                {
                    //this.logger.LogInformation(register.fullName + "Is Not Register");
                    return this.BadRequest(new { Status = false, Message = "User Not Register" });
                }
            }
            catch (Exception e)
            {
                {
                    //this.logger.LogInformation(register.fullName + "Has An Exception in Register");
                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }
        //Login Part
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
                //this.logger.LogInformation(Login.fullName + "Is Trying To Login");
                var response = await this.manager.Login(login);
                if (response != null)
                {

                    //this.logger.LogInformation(login.fullName + "Login Successfully");
                    string token = this.manager.GetJWTToken(login.emailID);
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "User Login Successfully", Token = token });
                }
                else

                {
                    //this.logger.LogInformation(login.fullName + "Is Not Register");
                    return this.BadRequest(new { Status = false, Message = "User Not Register" });
                }

            }
            catch (Exception e)
            {
                //this.logger.LogInformation(login.fullName + "Has An Exception in Register");
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        //Reset Part
        [HttpPut]
        [Route("reset")]
        public async Task<IActionResult> Reset(ResetModel reset)
        {
            try
            {
                //string emailID = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var response = await this.manager.Reset(reset);
                if (response != null)
                {
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "Password Change Successfully", Data = response });
                    
                }
                else

                {
                    return this.BadRequest(new { Status = false, Message = "Reset Password Failed", Data = response });

                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

    }
}
