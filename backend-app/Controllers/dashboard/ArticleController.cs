using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using backend_app.Services.dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/Article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticle article;
        public ArticleController(IArticle article)
        {
            this.article = article;
        }
        [HttpGet]
        [Route("GetArticle")]
        public async Task<ActionResult<Article>> GetArticle()
        {
            var result = await article.GetAllarticle();
            return Ok(result);
        }
        [HttpGet]
        [Route("getCreate")]
        public async Task<ActionResult<IEnumerable<SelectOption>>> GetCreate()
        {
            var result = await article.GetCreate();
            return Ok(result);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Article>> store([FromForm] ArticleImage articleImage)
        {
            if (await article.checktitle(articleImage.Title))
            {
                return BadRequest("An article with the same title already exists.");
            }

            var result = await article.store(articleImage);
            if (result != null)
            {
                return Ok(new
                {
                    message = "New Article Added Successfully"
                });
            }
            return BadRequest("false");
        }
        [HttpGet("{id}/edit")]
        public async Task<ActionResult<GetEditSelectOption<ArticleDTO>>> GetEditArticle(int id)
        {
            var result = await article.GetEditArticle(id);
            if (result != null)
            {
                return Ok(new
                {
                    data = result
                });
            }
            return BadRequest("An error occurred while updating the article");
        }
        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateArticle([FromForm] ArticleImage articleImage)
        {
            if (await article.checkUpdate(articleImage.Title, articleImage.Id))
            {
                return BadRequest("An article with the same title already exists.");
            }

            var result = await article.UpdateArticle(articleImage);
            if (result != null)
            {
                return Ok(new
                {
                    message = "The article has been updated successfully"
                });
            }
            return BadRequest("An error occurred while updating the article");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            try
            {
                bool isDeleted = await article.DeleteArticle(id);
                if (isDeleted)
                {
                    return Ok(new { message = "Article deleted successfully.", data = id });
                }
                else
                {
                    return NotFound(new { message = "Article not found." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, new { message = "An error occurred while deleting the article.", error = ex.Message });
            }
        }

    }
}
