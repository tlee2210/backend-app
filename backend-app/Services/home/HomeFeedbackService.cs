using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend_app.Services.home
{
    public class HomeFeedbackService : IHomeFeedbackServices
    {
        private readonly DatabaseContext db;

        public HomeFeedbackService(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Feedback> SendFeedback(Feedback feedback)
        {
            feedback.Status = "Unprocessed";
            feedback.CreateAt = DateTime.Now;
            var post = db.Feedbacks.Add(feedback);
            await db.SaveChangesAsync();
            return feedback;
        }
    }
}
