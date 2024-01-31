using System.Text.Json.Serialization;

namespace backend_app.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Slug { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public IEnumerable<Faculty>? Faculty { get; set; }
    }
}
