using backend_app.Models;

namespace backend_app.DTO
{
    public class LoginResult
    {
        public string Token { get; set; }
        public Account user { get; set; }
    }
}
