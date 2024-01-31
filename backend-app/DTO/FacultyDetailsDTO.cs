using backend_app.Models;

namespace backend_app.DTO
{
    public class FacultyDetailsDTO
    {
        public Faculty Faculty { get; set; }
        public List<List<DepartmentSemesterSession>> DepartmentSemesterSessionsGrouped { get; set; }
    }
}
