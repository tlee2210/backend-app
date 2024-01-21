using backend_app.Models;

namespace backend_app.DTO
{
    public class StaffDTO
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public string FileAvatar { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
    }
}
