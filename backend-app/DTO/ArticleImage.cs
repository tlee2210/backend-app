using System.ComponentModel.DataAnnotations.Schema;

namespace backend_app.DTO
{
    public class ArticleImage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        [NotMapped]
        public IFormFile? image { get; set; }
    }
}
