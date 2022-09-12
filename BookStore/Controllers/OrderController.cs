using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.OrderModel;
using System;
using System.Data;

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
    }

}
