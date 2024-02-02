using System.Text.Json.Serialization;

namespace backend_app.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Slug { get; set; }
        public string Skill_learn { get; set; }
        public string Opportunities { get; set; }
        public Double EntryScore { get; set; }
        public int  Course_id { get; set; }
        public string  Image { get; set; }
        public Courses? Courses { get; set; }
        [JsonIgnore]
        public ICollection<StudentFacultySemesters>? StudentFacultySemesters { get; set; }
    }
}
