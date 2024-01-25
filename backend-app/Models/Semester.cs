using backend_app.IRepository.dashboard;
using System.Text.Json.Serialization;

namespace backend_app.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public int SemesterNumber { get; set; } 
        public int AcademicYear { get; set; }
        [JsonIgnore]
        public ICollection<StudentFacultySemesters>? StudentFacultySemesters { get; set; }
    }
}
