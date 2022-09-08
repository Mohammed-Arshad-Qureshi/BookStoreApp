using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.UserModel;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UsersController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public ActionResult UserRegister(UserPostModel model)
        {
            try
            {
               var result =  this.userBL.AddUser(model);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successfull" });
                }
                return this.BadRequest(new { success = false, message = "Email Already Exits" });
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("Login")]
        public ActionResult UserLogin(UserLoginModel model)
        {
            try
            {
                var result = this.userBL.Login(model);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login Successfull" ,data=result});
                }
                return this.BadRequest(new { success = false, message = "Login Failed" });
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
