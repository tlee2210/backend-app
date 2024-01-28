using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/Semester")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ISemester service;

        public SemesterController(ISemester service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("GetCreate")]
        public async Task<ActionResult<IEnumerable<AllSelectOptionsDTO>>> GetCreate()
        {
            var result = await service.GetCteateSemester();
            return Ok(result);
        }
        [HttpGet]
        [Route("GetParameters")]
        public async Task<ActionResult<IEnumerable<AllSelectOptionsDTO>>> GetParameters()
        {
            var result = await service.GetParameters();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchParameters searchParameters)
        {
            var result = await service.Search(searchParameters);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<DepartmentSemesterSession>> Store(DepartmentSemesterSession departmentSemesterSession)
        {
            try
            {
                var exists = await service.Exists(departmentSemesterSession.FacultyId, departmentSemesterSession.SessionId, departmentSemesterSession.DepartmentId);
                if (exists)
                {
                    return BadRequest(new { message = "This course already exists in the curriculum." });
                }

                var result = await service.Store(departmentSemesterSession);

                if (result != null)
                {
                    return Ok(new { message = "DepartmentSemesterSession created successfully.", data = result });
                }
                else
                {
                    return BadRequest(new { message = "Unable to create the DepartmentSemesterSession." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the DepartmentSemesterSession record.", error = ex.Message });
            }
        }

    }
}
