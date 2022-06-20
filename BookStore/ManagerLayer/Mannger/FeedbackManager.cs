using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Mannger
{
    public class FeedbackManager : IFeedbackManager
    {
        private readonly IFeedbackRepository repo;
        public FeedbackManager(IFeedbackRepository repo)
        {
            this.repo = repo;
        }

        public async Task<FeedbackModel> AddToFeedback(FeedbackModel feedback)
        {
            try
            {
                return await this.repo.AddToFeedback(feedback);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<FeedbackModel> GetAllFeedback()
        {
            try
            {
                return this.repo.GetAllFeedback();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
