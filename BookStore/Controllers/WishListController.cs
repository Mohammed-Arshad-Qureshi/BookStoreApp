using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.WishListModel;
using System.Collections.Generic;
using System;
using System.Data;
using System.Security.Claims;
using System.Linq;

namespace BookStore.Controllers
{
    [Authorize(Roles = Roles.Users)]
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL _wishListBL;

        public WishListController(IWishListBL wishListBL)
        {
            _wishListBL = wishListBL;
        }

        [HttpPost("AddToWishList/{BookId}")]
        public IActionResult AddTOWishList(int BookId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = _wishListBL.AddToWishList(UserId, BookId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = "Check if Book is availbale OR it is already to the WishList!!" });
                }

                return this.Ok(new { success = true, Message = "Book Added to WishList Sucessfull..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllWishList")]
        public IActionResult GetAllWishList()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = _wishListBL.GetAllWishList(UserId);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = $"No Book available in WishList!!" });
                }

                return this.Ok(new { success = true, Message = $"Books in WishList fetched Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("DeleteBookFromWishList/{WishListId}")]
        public IActionResult DeleteBookFromWishList(int WishListId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = _wishListBL.DeleteWishListItem(UserId, WishListId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = "Something went wrong while removing Book from the WishList!!" });
                }

                return this.Ok(new { success = true, Message = $"Book removed from WishList Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
