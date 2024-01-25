using backend_app.Models;

namespace backend_app.DTO
{
    public class StudentLoginResult
    {
        public string Token { get; set; }
        public Students student { get; set; }
    }
}
