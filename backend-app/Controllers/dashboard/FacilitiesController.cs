using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/Facilitie")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private readonly IFacilitie facilitie;

        public FacilitiesController(IFacilitie facilitie)
        {
            this.facilitie = facilitie;
        }
        [HttpGet]
        [Route("Getfacilities")]
        public async Task<ActionResult<Facilities>> GetArticle()
        {
            var result = await facilitie.GetList();
            return Ok(result);
        }
        [HttpPost]
        [Route("Store")]
        public async Task<IActionResult> StoreFacilities([FromForm] FacilitieImage facilitieImage)
        {
            if (await facilitie.checkTitle(facilitieImage))
            {
                return BadRequest("An Faculty with the same name already exists.");
            }
            var result = await facilitie.store(facilitieImage);
            if (result != null)
            {
                return Ok(new { message = "New facilitie Added Successfully" });
            }
            return BadRequest("The name directory already exists.");
        }
        [HttpGet("{id}/edit")]
        public async Task<ActionResult<Facilities>> GetEditFacilities(int id)
        {
            var result = await facilitie.GetEdit(id);
            if (result != null)
            {
                return Ok(new
                {
                    data = result
                });
            }
            return NotFound("Facility not found");
        }
        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateFacilities([FromForm] FacilitieImage facilitieImage)
        {
            if (await facilitie.checkTitle(facilitieImage))
            {
                return BadRequest("An Faculty with the same name already exists.");
            }
            var result = await facilitie.UpdateFacilitie(facilitieImage);
            if (result != null)
            {
                return Ok(new
                {
                    message = "The facilitie has been updated successfully"
                });
            }
            return BadRequest("An error occurred while updating the facilitie");
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool isDeleted = await facilitie.Delete(id);
                if (isDeleted)
                {
                    return Ok(new { message = "facilitie deleted successfully.", data = id });
                }
                else
                {
                    return NotFound(new { message = "facilitie not found." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, new { message = "An error occurred while deleting the facilitie.", error = ex.Message });
            }
        }
    }
}
