namespace backend_app.Models
{
    public class StudentFacultySemesters
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int FacultyId { get; set; }
        public int SemesterId { get; set; }
        public virtual Students Student { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual Semester Semester { get; set; }
    }
}
