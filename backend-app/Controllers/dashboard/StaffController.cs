using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private IStaff service;
        public StaffController(IStaff service)
        {
            this.service = service;
        }

        [HttpGet("GetList")]
        public async Task<IEnumerable<StaffDTO>> GetList()
        {
            return await service.GetAllStaffs();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Staff>> PostCourse([FromForm] StaffImage staff)
        {
            var result = await service.AddStaff(staff);
            if (result != null)
            {
                return Ok(new
                {
                    message = "New staff member has been successfully added."
                });
            }
            return BadRequest("Unable to add new staff member. Please check the input information again.");
        }

        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> DeleteStaff(int id)
        {
            var result = await service.DeleteStaff(id);
            if (result != null)
            {
                return Ok(new
                {
                    message = "Staff member has been deleted successfully.", data = result
                });
            }
            return BadRequest("Unable to delete the staff member. Please try again.");
        }

        [HttpPost("Update")]
        public async Task<ActionResult<Staff>> PutStaff([FromForm] StaffImage staffImage)
        {
            try
            {
                var updatedStaff = await service.UpdateStaff(staffImage);
                if (updatedStaff != null)
                {
                    return Ok(new
                    {
                        message = "Staff record updated successfully"
                    });
                }
                return NotFound("Staff not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while updating the staff record");
            }
        }
        [HttpGet("{id}/GetEdit")]
        public async Task<StaffDTO> GetOne(int id)
        {
            return await service.GetEdit(id);
        }
    }
}
