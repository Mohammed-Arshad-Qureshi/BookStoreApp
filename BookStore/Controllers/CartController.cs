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
    [Authorize(Roles = Roles.Users)]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL _cartBL;

        public CartController(ICartBL CartBL)
        {
            _cartBL = CartBL;
        }


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

        [HttpGet("GetAllBooksInCart")]
        public IActionResult GetAllBooksInCart()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                List<CartResponseModel> result = _cartBL.GetAllBooksInCart(UserId);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = $"No Book available in cart!!" });
                }

                return this.Ok(new { success = true, Message = $"Books in Cart fetched Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllBooksInCartByCardId/{CartId}")]
        public IActionResult GetAllBooksInCartByCartId(int CartId)
         {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                CartResponseModel result = _cartBL.GetAllBooksInCartByCartId(UserId,CartId);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = $"No Book available in cart!!" });
                }

                return this.Ok(new { success = true, Message = $"Books in Cart fetched Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPut("UpdateCartItem")]
        public IActionResult UpdateCartItem(CartUpdateModel cartUpdateModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = _cartBL.UpdateCartItem(UserId, cartUpdateModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Update cart Failed!! Check if CartItem is availbale in Cart or not..." });
                }

                return this.Ok(new { success = true, Message = $"CartId : {cartUpdateModel.CartId} Updated In cart Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("DeleteCart/{CartId}")]
        public IActionResult DeleteCart(int CartId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = _cartBL.DeleteCartbyCartId(UserId, CartId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Something went wrong while removing CartItemId : {CartId} from the cart!!" });
                }

                return this.Ok(new { success = true, Message = $"CartItemId : {CartId} removed from cart Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
  