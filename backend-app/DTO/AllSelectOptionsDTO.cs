namespace backend_app.DTO
{
    public class AllSelectOptionsDTO
    {
        public List<SelectOption> FacultyOptions { get; set; } = new List<SelectOption>();
        public List<SelectOption> DepartmentOptions { get; set; } = new List<SelectOption>();
        public List<SelectOption> SessionOptions { get; set; } = new List<SelectOption>();
        public List<SelectOption> SemesterOptions { get; set; } = new List<SelectOption>();
    }
}
