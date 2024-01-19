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

        [HttpGet("{id}/edit")]
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
        public async Task<ActionResult> DeleteCourse(int id)
        {
            var result = await service.DeleteDepartment(id);
            if (result != null)
            {
                return Ok(new
                {
                    message = "Department Deleted Successfully", data = result.Id
                });
            }
            return BadRequest("Delete fail");
        }

        [HttpPost("update")]
        public async Task<ActionResult<Department>> PutCourse(Department department)
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
            var result = await service.UpdateDepartment(department);
            if (result != null)
            {
                return Ok(new
                {
                    message = "Department Update Successfully",
                    data = result
                });
            }
            return BadRequest("Update fail");
        }
    }
}
