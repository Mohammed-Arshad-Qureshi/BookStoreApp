using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Linq;
using ModelLayer.FeedBack;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackBL _feedBackBL;

        public FeedBackController(IFeedBackBL feedBackBL)
        {
            _feedBackBL = feedBackBL;
        }

        [Authorize]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(FeedBackPostModel postModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = _feedBackBL.AddFeedback(UserId, postModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = "You have already added the Feedback for this Book" });
                }

                return this.Ok(new { success = true, Message = "Feedback Added Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
