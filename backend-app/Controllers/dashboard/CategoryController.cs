using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategory Category;

        public CategoryController(ICategory category)
        {
            Category = category;
        }
        [HttpGet]
        public async Task<ActionResult<Category>> GetCategories()
        {
            var result = await Category.GetAllCategory();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategories(string name)
        {
            var result = await Category.CreateCategory(name);
            if(result != null)
            {
                return Ok(new { message = "New Category Added Successfully", data = result });
            }
            return BadRequest("The name directory already exists.");
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetEditCategories(int id)
        {
            var result = await Category.getCategoryEdit(id);
            if(result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategories(Category category)
        {
            var result = await Category.UpdateCategory(category);
            if(result != null)
            {
                return Ok(new { message = "Update Category Successfully", data = result });
            }
            return BadRequest("Update fail");
        }
        [HttpDelete("id")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            var result = await Category.CategoryDelete(id);
            if(result != null)
            {
                return Ok(new { message = "Delete Category Successfully", data = result });
            }
            return BadRequest("delete fail");
        }
        
    }
}
