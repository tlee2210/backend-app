using backend_app.Models;

namespace backend_app.DTO
{
    public class StudentDTO
    {
        public int? Id { get; set; }
        public string StudentCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public virtual StudentFacultySemesters? StudentFacultySemesters { get; set; }
    }
}
