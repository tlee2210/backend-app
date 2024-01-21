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
            var result = service.UpdateStaff(staffImage);
            if (result != null)
            {
                return Ok(new
                {
                    message = "New Article Added Successfully"
                });
            }
            return BadRequest("false");
        }
        [HttpGet("{id}/GetEdit")]
        public async Task<StaffDTO> GetOne(int id)
        {
            return await service.GetEdit(id);
        }
    }
}
