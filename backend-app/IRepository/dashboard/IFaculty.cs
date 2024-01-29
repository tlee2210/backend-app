using backend_app.DTO;
using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface IFaculty
    {
        Task<IEnumerable<FacultyDTO>> GetAllFaculties();
        Task<FacultyDTO> GetOneFaculty(int id);
        Task<GetEditSelectOption<Faculty>> GetEditFaculty(int id);
        Task<IEnumerable<SelectOption>> GetCreate();
        Task<Faculty> AddFaculties(FacultyImage faculty);
        Task<Faculty> UpdateFaculty(FacultyImage facultyImg);
        Task<bool> DeleteFaculties(int id);
        Task<bool> CheckIfCodeExists(FacultyImage facultyImg);
        Task<bool> CheckIfTitleExists(FacultyImage facultyImg);
        Task<IEnumerable<Faculty>> SearchFaculty(string title);
    }
}
