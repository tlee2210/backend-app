using backend_app.IRepository.dashboard;

namespace backend_app.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public int SemesterNumber { get; set; } 
        public int AcademicYear { get; set; }
    }
}
