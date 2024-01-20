using backend_app.DTO;
using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface IFacilitie
    {
        //Task<IEnumerable<Facilities>> GetAll();
        //Task<bool> DeleteFaciliti(int Id);
        Task<Facilities> store(FacilitieImage facilitieImage);
        Task<Facilities> UpdateFacilitie(FacilitieImage facilitieImage);
        Task<bool> checkTitle(FacilitieImage FacilitieImage);
        Task<IEnumerable<Facilities>> GetList();
        Task<Facilities> GetEdit(int id);
        Task<bool> Delete(int id);
    }
}
