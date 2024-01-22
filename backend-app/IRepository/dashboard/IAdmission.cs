using backend_app.Models;
using System.Threading.Tasks;

namespace backend_app.IRepository.dashboard
{
    public interface IAdmission
    {
        Task<IEnumerable<Admission>> GetAllProccess();
        Task<IEnumerable<Admission>> GetAllAccept();
        Task<IEnumerable<Admission>> GetAllReject();
        Task<Admission> GetOneAdmission(int id);
        Task<Admission> AcceptAdmission(int id);
        Task<Admission> RejectAdmission(int id);
        Task<Admission> DeleteAdmission(int id);
    }
}
