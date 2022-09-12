using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.AddressModel;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Linq;
using BusinessLayer.Interfaces;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL _addressBL;

        public AddressController(IAddressBL addressBL)
        {
            _addressBL = addressBL;
        }


        [HttpPost("AddAddress")]
        public IActionResult AddAddress(AddressPostModel postModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = _addressBL.AddAddress(UserId, postModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = "Someting Went Wrong While adding Address" });
                }

                return this.Ok(new { success = true, Message = "Address Added Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
