using System.ComponentModel.DataAnnotations.Schema;

namespace backend_app.DTO
{
    public class FacilitieImage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desciption { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}
