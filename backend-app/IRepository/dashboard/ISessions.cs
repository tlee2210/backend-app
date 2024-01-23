using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface ISessions
    {
        Task<IEnumerable<Session>> GetAllSessions();
        Task<Session> GetOneSession(int id);
        Task<Session> AddSession();
        Task<IEnumerable<Session>> UpdateSessionsStatusAndCurrentYear();
        Task<Session> DeleteSession(int id);
    }
}
