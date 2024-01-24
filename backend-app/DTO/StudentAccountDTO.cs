using backend_app.Models;

namespace backend_app.DTO
{
    public class StudentAccountDTO
    {
        public int? Id { get; set; }
        public string StudentCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
