using backend_app.Models;

namespace backend_app.DTO
{
    public class AdmissionDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string HighSchool { get; set; }
        public double GPA { get; set; }
        public int FacultyId { get; set; }
    }
}
