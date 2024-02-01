using backend_app.Models;

namespace backend_app.DTO
{
    public class StudentprofileDTO
    {
        public Students students { get; set; }
        public List<List<DepartmentSemesterSession>> DepartmentSemesterSessionsGrouped { get; set; }
    }
}
