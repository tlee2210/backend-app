using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/Courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private ICourses service;
        public CoursesController(ICourses services)
        {
            this.service = services;
        }
        [HttpGet("{id}")]
        public async Task<Courses> GetOneCourse(int id)
        {
            return await service.GetOneCourse(id);
        }
        [HttpGet("GetList")]
        public async Task<IEnumerable<Courses>> GetList()
        {
            return await service.GetAllCourses();
        }
        [HttpPost]
        public async Task<IActionResult> StoreCourse(Courses courses)
        {
            var result = await service.AddCourses(courses);
            if (result != null)
            {
                return Ok(new { message = "New Course Added Successfully", data = result });
            }
            return BadRequest("The name directory already exists.");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Courses>> DeleteCourse(int id)
        {
            var result = await service.DeleteCourses(id);
            if (result != null)
            {
                return Ok(new { message = "Delete Courses Successfully", data = result });
            }
            return BadRequest(new { message = "Delete Courses fail" });
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCourse([FromForm] Courses courses)
        {
            var result = await service.UpdateCourses(courses);
            if (result != null)
            {
                return Ok(new { message = "Update Course Successfully", data = result });
            }
            return BadRequest(new { message = "Update Courses fail" });
        }
    }
}
