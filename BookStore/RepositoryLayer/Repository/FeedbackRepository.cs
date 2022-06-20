using Microsoft.Extensions.Configuration;
using ModelLayer;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IMongoCollection<FeedbackModel> Feedback;
        private readonly IConfiguration configuration;

        public FeedbackRepository(IDBSetting db, IConfiguration configuration)
        {

            this.configuration = configuration;
            var userclient = new MongoClient(db.ConnectionString);
            var database = userclient.GetDatabase(db.DatabaseName);
            Feedback = database.GetCollection<FeedbackModel>("Feedback");
        }

        public async Task<FeedbackModel> AddToFeedback(FeedbackModel feedback)
        {
            try
            {
                var check = await this.Feedback.Find(x => x.feedbackID == feedback.feedbackID).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this.Feedback.InsertOneAsync(feedback);
                    return feedback;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        IEnumerable<FeedbackModel> IFeedbackRepository.GetAllFeedback()
        {
            return Feedback.Find(FilterDefinition<FeedbackModel>.Empty).ToList();
        }
    }
}
