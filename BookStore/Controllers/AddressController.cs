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

        [HttpGet("GetAllAddress")]
        public IActionResult GetAllAddress()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = _addressBL.GetAllAddresses(UserId);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = $"No Addresses available For UserId : {UserId}!!" });
                }

                return this.Ok(new { success = true, Message = $"Addresses List of UserId : {UserId} fetched Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("UpdateAddress")]
        public IActionResult UpdateAddressbyId(AddressUpdateModel postModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = _addressBL.UpdateAddressbyId(UserId, postModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Update Address Failed!! Check if Address is already availbale..." });
                }

                return this.Ok(new { success = true, Message = $"AddressId : {postModel.AddressId} Updated Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("DeleteAddress/{AddressId}")]
        public IActionResult DeleteAddressByAddressId(int AddressId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = _addressBL.DeleteAddressByAddressId(AddressId, UserId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = "Something went wrong while deleting Address}!!" });
                }

                return this.Ok(new { success = true, Message = "Address Deleted Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllAddressById/{AddressId}")]
        public IActionResult GetAllAddress(int AddressId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = _addressBL.GetAllAddressById(UserId,AddressId);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = $"No Address Found !!" });
                }

                return this.Ok(new { success = true, Message = $"Address fetched Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
