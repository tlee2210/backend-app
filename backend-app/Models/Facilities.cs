using System.ComponentModel.DataAnnotations;

namespace backend_app.Models
{
    public class Facilities
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
       
    }
}
