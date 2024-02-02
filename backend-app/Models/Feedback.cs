namespace backend_app.Models
{
    public class Feedback
    {
        private string _status = "Unprocessed";
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string responses { get; set; } = "";

        public string Status
        {
            get => _status;
            set
            {
                if (value == "Processed" || value == "Unprocessed")
                {
                    _status = value;
                }
                else
                {
                        throw new ArgumentException("Invalid status value");
                }
            }
        }
        public DateTime CreateAt { get; set; } = DateTime.Now;

    }
}
