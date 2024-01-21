namespace backend_app.Models
{
    public class Students
    {
        public int Id { get; set; }
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
        public int? FacultyId { get; set; }
        public int? CurrentSemesterId { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual Semester CurrentSemester { get; set; }

    }
    public enum Gender
    {
        Male,
        Female,
    }

}
