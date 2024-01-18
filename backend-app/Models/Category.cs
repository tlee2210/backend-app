using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend_app.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<ArticleCategory>? ArticleCategories { get; set; }
    }
}
