using backend_app.Models;
using System.Security.Claims;

namespace backend_app.IRepository.home
{
    public interface IHomeFeedbackServices
    {
        Task<Feedback> SendFeedback(ClaimsPrincipal users, string feedbackContent);
    }
}
