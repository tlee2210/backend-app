using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using backend_app.Services.dashboard;
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
            var dividedResults = await service.DivideInto8Semesters(searchParameters);
            return Ok(dividedResults);
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
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteDepartmentSemesterSession(int id)
        {
            try
            {
                var deleted = await service.Delete(id);
                if (deleted)
                {
                    return Ok(new { message = "DepartmentSemesterSession deleted successfully." });
                }
                else
                {
                    return NotFound(new { message = "DepartmentSemesterSession not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the DepartmentSemesterSession.", error = ex.Message });
            }
        }

        [HttpPost("CreateSemesters")]
        public async Task<IActionResult> CreateSemesters([FromBody] CreateSemesterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdSemesters = await service.CreateSemesters(request.FacultyId, request.semesterId, request.DepartmentIds);
                return Ok(new { message = "Semesters created successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "An internal server error occurred.");
            }
        }

    }
}
