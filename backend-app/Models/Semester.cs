using backend_app.IRepository.dashboard;

namespace backend_app.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 

        //public virtual ICollection<Course> Courses { get; set; } // Courses in this semester

        //public virtual ICollection<Enrollment> Enrollments { get; set; } // Enrollments in this semester
    }
}
