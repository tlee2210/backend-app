using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/Faculty")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private IFaculty service;
        public FacultyController(IFaculty services)
        {
            this.service = services;
        }
        [HttpGet("{id}/edit")]
        public async Task<GetEditSelectOption<Faculty>> GetEditFaculty(int id)
        {
            //return await service.GetOneFaculty(id);
            return await service.GetEditFaculty(id);
        }
        [HttpGet("GetList")]
        public async Task<IEnumerable<FacultyDTO>> GetList()
        {
            return await service.GetAllFaculties();
        }
        [HttpGet]
        [Route("GetCreate")]
        public async Task<ActionResult<IEnumerable<SelectOption>>> GetCreate()
        {
            var result = await service.GetCreate();
            return Ok(result);
        }
        [HttpPost]
        [Route("Store")]
        public async Task<IActionResult> StoreFaculty([FromForm] FacultyImage facultyImg)
        {
            var faculty = new Faculty
            {
                Code = facultyImg.Code,
                Title = facultyImg.Title,
            };
            if (await service.checkCode(facultyImg))
            {
                return BadRequest("An Faculty with the same Code already exists.");
            }
            if (await service.checkTitle(facultyImg))
            {
                return BadRequest("An Faculty with the same name already exists.");
            }
            var result = await service.AddFaculties(facultyImg);
            if (result != null)
            {
                return Ok(new { message = "New Faculty Added Successfully", data = result });
            }
            return BadRequest("The name directory already exists.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaculties(int id)
        {
            try
            {
                var isDeleted = await service.DeleteFaculties(id);
                if (isDeleted)
                {
                    return Ok(new { message = "Faculties deleted successfully.", data = id });
                }
                else
                {
                    return NotFound(new { message = "Faculties not found." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, new { message = "An error occurred while deleting the article.", error = ex.Message });
            }

        }
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateFaculty([FromForm] FacultyImage facultyImg)
        {
            var faculty = new Faculty
            {
                Code = facultyImg.Code,
                Title = facultyImg.Title,
            };
            if (await service.checkCode(facultyImg))
            {
                return BadRequest("An Faculty with the same Code already exists.");
            }
            if (await service.checkTitle(facultyImg))
            {
                return BadRequest("An Faculty with the same name already exists.");
            }

            var result = await service.UpdateFaculty(facultyImg);
            if (result != null)
            {
                return Ok(new
                {
                    message = "The Faculty has been updated successfully"
                });
            }
            return BadRequest("An error occurred while updating the Faculty");
        }

        [HttpGet("Search/{title}")]
        public async Task<IActionResult> Search(string title)
        {
            var _result = await service.SearchFaculty(title);
            if (_result != null)
            {
                return Ok(new
                {
                    message = "Search successfully",
                    result = _result
                });
            }
            return BadRequest("Not found");
        }
    }
}
