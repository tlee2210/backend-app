using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudent service;
        public StudentController(IStudent service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Students>> PostStudents([FromForm] StudentImage student)
        {
            var result = await service.AddStudent(student);
            if (result != null)
            {
                return Ok(new
                {
                    message = "New student has been successfully added."
                });
            }
            return BadRequest("Unable to add new student. Please check the input information again.");
        }

        [HttpGet("GetList")]
        public async Task<IEnumerable<StudentDTO>> GetList()
        {
            return await service.GetAllStudents();
        }

        [HttpGet]
        [Route("GetCreate")]
        public async Task<ActionResult<IEnumerable<SelectOption>>> GetCreate()
        {
            var result = await service.GetCreate();
            return Ok(result);
        }

        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var result = await service.DeleteStudent(id);
            if (result != null)
            {
                return Ok(new
                {
                    message = "Student has been deleted successfully.",
                    data = result
                });
            }
            return BadRequest("Unable to delete the student. Please try again.");
        }

        [HttpPost("Update")]
        public async Task<ActionResult<Students>> PutStudent([FromForm] StudentImage student)
        {
            var result = service.UpdateStudent(student);
            if (result != null)
            {
                return Ok(new
                {
                    message = "New Student Updated Successfully"
                });
            }
            return BadRequest("false");
        }
        [HttpGet("{id}/GetEdit")]
        public async Task<StudentDTO> GetOne(int id)
        {
            return await service.GetEdit(id);
        }
    }
}
