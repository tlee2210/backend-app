using backend_app.IRepository.dashboard;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.home
{
    [Route("api/home/courses")]
    [ApiController]
    public class CoursesHomeController : ControllerBase
    {
        private ICoursesHome service;
        public CoursesHomeController(ICoursesHome services)
        {
            this.service = services;
        }

        [HttpGet("GetList")]
        public async Task<IEnumerable<Courses>> GetList()
        {
            return await service.GetAllCourses();
        }

    }
}
