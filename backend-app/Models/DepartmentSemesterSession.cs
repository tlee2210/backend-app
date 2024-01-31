using System.ComponentModel.DataAnnotations;

namespace backend_app.Models
{
    public class DepartmentSemesterSession
    {
        [Key]
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int SemesterId { get; set; }
        public int SessionId { get; set; }
        public int FacultyId { get; set; }
        public Department? Department { get; set; }
        public Semester? Semester { get; set; }
        public Session? session { get; set; }
        public Faculty? Faculty { get; set; }
    }
}
