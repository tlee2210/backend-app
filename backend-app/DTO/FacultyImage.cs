using System.ComponentModel.DataAnnotations.Schema;

namespace backend_app.DTO
{
    public class FacultyImage
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Slug { get; set; }
        public string Skill_learn { get; set; }
        public string Opportunities { get; set; }
        public int EntryScore { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public int Course_id { get; set; }
        public string CoursesName { get; set; }
    }
}
