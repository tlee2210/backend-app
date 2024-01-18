using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_app.Models
{
    public class ArticleCategory
    {
        [Key]
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public Article? Article { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
