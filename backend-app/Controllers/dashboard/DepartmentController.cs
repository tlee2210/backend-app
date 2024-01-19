using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/department")]
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
        [Route("create")]
        public async Task<ActionResult> PostCourse(Department department)
        {
            if (await service.checkCode(department))
            {
                return BadRequest(new
                {
                    message = "An Department with the same code already exists."
                });
            }
            if (await service.checkSubject(department))
            {
                return BadRequest(new
                {
                    message = "An Department with the same subject already exists."
                });
            }
            var result = await service.AddDepartment(department);
            if (result != null)
            {
                return Ok(new
                {
                    message = "New Department Added Successfully"
                });
            }
            return BadRequest("false");
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
