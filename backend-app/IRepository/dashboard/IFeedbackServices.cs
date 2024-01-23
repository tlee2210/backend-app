using backend_app.DTO;
using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface IFeedbackServices
    {
        Task<Feedback> GetFeedback(int id);
        Task<IEnumerable<Feedback>> GetListUnprocess();
        Task<IEnumerable<Feedback>> GetListProcess();
        Task<Feedback> UpdateFeedback(Feedback feedback);
    }
}
