namespace backend_app.Models
{
    public class StudentAccount
    {
        public int? Id { get; set; }
        public string StudentCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
