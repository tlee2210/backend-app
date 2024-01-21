using backend_app.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_app.DTO
{
    public class StaffImage
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        [NotMapped]
        public IFormFile? FileAvatar { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
