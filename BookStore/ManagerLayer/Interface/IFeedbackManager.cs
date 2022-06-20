using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IFeedbackManager
    {
        Task<FeedbackModel> AddToFeedback(FeedbackModel feedback);
        IEnumerable<FeedbackModel> GetAllFeedback();
    }
}
