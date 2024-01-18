using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IDepartment service;
        public DepartmentController(IDepartment services)
        {
            this.service = services;
        }

        [HttpGet("{id}")]
        public async Task<Department> GetOneDepartment(int id)
        {
            return await service.GetOneDepartment(id);
        }

        [HttpGet("GetList")]
        public async Task<IEnumerable<Department>> GetList()
        {
            return await service.GetAllDepartments();
        }

        [HttpPost]
        public async Task<Department> PostCourse(Department department)
        {
            return await service.AddDepartment(department);
        }

        [HttpDelete("{id}")]
        public async Task<Department> DeleteCourse(int id)
        {
            return await service.DeleteDepartment(id);
        }

        [HttpPut]
        public async Task<Department> PutCourse(Department courses)
        {
            return await service.UpdateDepartment(courses);
        }
    }
}
