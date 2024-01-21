namespace backend_app.Models
{
    public class StudentFacultySemesters
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string FacultyId { get; set; }
        public string SemesterId { get; set; }
        public virtual Students Student { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual Semester Semester { get; set; }
    }
}
