using ModelLayer.FeedBack;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedBackRL
    {
        public bool AddFeedback(int UserId, FeedBackPostModel feedbackModel);
        public List<FeedBackResponseModel> GetAllFeedbacksByBookId(int BookId);

    }
}
