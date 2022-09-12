using BusinessLayer.Interfaces;
using ModelLayer.FeedBack;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedBackBL:IFeedBackBL
    {
        private readonly IFeedBackRL _feedBackRL;

        public FeedBackBL(IFeedBackRL feedBackRL)
        {
            _feedBackRL = feedBackRL;
        }

        public bool AddFeedback(int UserId, FeedBackPostModel feedbackModel)
        {
            try
            {
                return _feedBackRL.AddFeedback(UserId, feedbackModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
