using backend_app.Models;

namespace backend_app.IRepository.home
{
    public interface IHomeFeedbackServices
    {
        Task<Feedback> SendFeedback(Feedback feedback);
    }
}
