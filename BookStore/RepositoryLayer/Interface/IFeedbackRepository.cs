using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRepository
    {
        Task<FeedbackModel> AddToFeedback(FeedbackModel feedback);
        IEnumerable<FeedbackModel> GetAllFeedback();
    }
}
