namespace backend_app.Models
{
    public class Admission
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string HighSchool { get; set; }
        public double GPA { get; set; }
        public string EnrollmentNumber { get; set; }
        public string? Status { get; set; }
        public int FacultyId { get; set; }
        public Faculty? Faculty { get; set; }
    }
}
