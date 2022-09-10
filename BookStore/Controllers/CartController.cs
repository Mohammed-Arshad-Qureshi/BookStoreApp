using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.CartModel;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL _cartBL;

        public CartController(ICartBL CartBL)
        {
            _cartBL = CartBL;
        }


        [Authorize]
        [HttpPost("AddBookToCart")]
        public IActionResult AddBookTOCart(CartPostModel postModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = _cartBL.AddBookTOCart(UserId, postModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Check if Book is availbale OR it is already in Cart!!  BookId : {postModel.BookId} to the cart!!" });
                }

                return this.Ok(new { success = true, Message = $"BookId : {postModel.BookId} Added to cart Sucessfull..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
