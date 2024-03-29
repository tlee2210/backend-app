﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend_app.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string image { get; set; }
        public DateTime PublishDate { get; set; }
        [JsonIgnore]
        public ICollection<ArticleCategory>? ArticleCategories { get; set; }
    }
}
