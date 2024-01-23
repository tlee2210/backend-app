namespace backend_app.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime YearStart { get; set; }
        public DateTime YearEnd { get; set; }
        public bool IsCurrentYear { get; set; } = false;
    }
}
