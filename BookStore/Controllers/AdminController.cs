using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.AdminModel;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL _adminBL;

        public AdminController(IAdminBL adminBL)
        {
            _adminBL = adminBL;
        }

        [HttpPost("Login")]
        public IActionResult UserLogin(AdminLoginModel loginModel)
        {
            try
            {
                var result = _adminBL.AdminLogin(loginModel);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = "Login Failed!! Check your EmailId and Password and try again..." });
                }

                return this.Ok(new { success = true, Message = "ADMIN LOGIN SUCCESSFULL!!", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
