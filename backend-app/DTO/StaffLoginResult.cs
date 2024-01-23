namespace backend_app.DTO
{
    public class StaffLoginResult
    {
        public string Token { get; set; }
        public StaffAccountDTO staff { get; set; }
    }
}
