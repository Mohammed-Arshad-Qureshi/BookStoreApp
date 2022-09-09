using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
                    return this.Ok(new { success = true, message = "USER REGISTRATION SUCCESSFULL" });
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
                    return this.Ok(new { success = true, message = "USER LOGIN SUCCESSFULL" ,data=result});
                }
                return this.BadRequest(new { success = false, message = "LOGIN FAILED!! Check your EmailId and Password and try again..." });
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("ForgetPassword/Email")]
        public ActionResult ForgetPassword(string Email)
        {
            try
            {
                bool result = this.userBL.ForgetPasswordUser(Email);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Reset Password Link Send Successfully" });
                }
                return this.BadRequest(new { success = false, message = "Unable to send Reset Password Link" });
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpPut("ResetPassword")]
        public ActionResult ResetPassword(UserResetPasswordModel PasswordModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var email = claims.Where(p => p.Type == @"Email").FirstOrDefault()?.Value;
                bool result = this.userBL.ResetPassoword(email,PasswordModel);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Password Changed Successfully" });
                }
                return this.BadRequest(new { success = false, message = "Unable to Change Password" });
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
