using backend_app.Models;

namespace backend_app.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string image { get; set; }
        public DateTime PublishDate { get; set; }
        public List<CategoryDTO> Categories { get; set; }

    }
}
