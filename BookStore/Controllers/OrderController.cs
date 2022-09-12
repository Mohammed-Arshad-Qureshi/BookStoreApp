using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.OrderModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Authorize(Roles = Roles.Users)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL _orderBL;

        public OrderController(IOrderBL orderBL)
        {
            _orderBL = orderBL;
        }

        [HttpPost("AddOrder")]
        public IActionResult AddOrder(OrderPostModel postModel)
        {
            try
            {
                var result = _orderBL.AddOrder(postModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Check if Book is availbale in cart OR Check enough Books are in stock !! OR Check AddressId Exists!!" });
                }

                return this.Ok(new { success = true, Message = $"Order placed Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                List<OrderResponseModel> result = _orderBL.GetAllOrders(UserId);
                if (result.Count == 0)
                {
                    return this.BadRequest(new { success = false, Message = $"No Addresses available For UserId : {UserId}!!" });
                }

                return this.Ok(new { success = true, Message = $"Order List of UserId : {UserId} fetched Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
