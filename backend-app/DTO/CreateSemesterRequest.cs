namespace backend_app.DTO
{
    public class CreateSemesterRequest
    {
        public int FacultyId { get; set; }
        public int semesterId { get; set; }
        public int[] DepartmentIds { get; set; }
    }
}
