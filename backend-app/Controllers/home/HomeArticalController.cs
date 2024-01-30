using backend_app.DTO;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace backend_app.Controllers.home
{
    [Route("api/home/homeartical")]
    [ApiController]
    public class HomeArticalController : ControllerBase
    {
        private readonly IHomeArtical homeArtical;
        public HomeArticalController(IHomeArtical homeArtical)
        {
            this.homeArtical = homeArtical;
        }
        [HttpGet]
        public async Task<IActionResult> GetListArtical()
        {
            var artical = await homeArtical.GetList();
            if(artical != null)
            {
                return Ok(artical);
            }
         return NotFound();
        }

        [HttpGet("GetDetail/{id}")]
        public async Task<DetailsWithRelatedDTO<ArticleDTO, ArticleDTO>> GetDetail(int id)
        {
            return await homeArtical.GetDetail(id);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string search = "")
        {
            var searchResults = await homeArtical.Search(search);

            return Ok(searchResults);
        }
        [HttpGet("SearchByCategory/{categoryId}")]
        public async Task<IActionResult> SearchByCategory(int categoryId)
        {
            var searchResults = await homeArtical.SearchByCategory(categoryId);

            if (searchResults == null || !searchResults.Any())
            {
                return NotFound($"No articles found for category ID {categoryId}.");
            }

            return Ok(searchResults);
        }

    }
}
