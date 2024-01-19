using backend_app.DTO;
using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface IFacilitie
    {
        Task<IEnumerable<Facilities>> GetAll();
        Task<bool> DeleteFaciliti(int Id);
        Task<Facilities> store(FacilitieImage facilitieImage);
        Task<Facilities> UpdateFacilitie(FacilitieImage facilitieImage);
        Task<bool> checktitle(string title);
        Task<bool> checkUpdate(string title, int id);
    }
}
