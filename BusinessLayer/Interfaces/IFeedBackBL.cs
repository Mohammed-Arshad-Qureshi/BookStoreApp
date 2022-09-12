using ModelLayer.FeedBack;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IFeedBackBL
    {
        public bool AddFeedback(int UserId, FeedBackPostModel postModel);
        public List<FeedBackResponseModel> GetAllFeedbacksByBookId(int BookId);
        public bool DeleteFeedbackById(int FeedBackId);
    }
}
