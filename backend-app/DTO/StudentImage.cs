using backend_app.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_app.DTO
{
    public class StudentImage
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [NotMapped]
        public IFormFile? Avatar { get; set; }
        public string? Password { get; set; }
        public int FacultyId { get; set; }
    }
}
