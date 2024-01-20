using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/Faciliti")]
    [ApiController]
    public class FacilitieController : ControllerBase
    {
        private readonly IFacilitie _facilitieRepo;
        public FacilitieController(IFacilitie facilitieRepo)
        {
           this._facilitieRepo = facilitieRepo;
        }
        [HttpGet]
        [Route("getlist")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _facilitieRepo.GetAll());

        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteFacilitie(int Id)
        {
            try
            {
                var result = await _facilitieRepo.DeleteFaciliti(Id);
                if (result)
                {
                    return Ok("Delete success");
                }
                return BadRequest("Delete fail");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Facilities>> store([FromForm] FacilitieImage facilityImage)
        {
            if (await _facilitieRepo.checktitle(facilityImage.Title))
            {
                return BadRequest("A facility with the same title already exists.");
            }

            try
            {
                var result = await _facilitieRepo.store(facilityImage);
                if (result != null)
                {
                    return Ok(new
                    {
                        message = "New facility added successfully."
                    });
                }
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the new facility.");
            }

            return BadRequest("Unable to add the new facility.");
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateFacilitie([FromForm] FacilitieImage facilitieImage)
        {
            if (await _facilitieRepo.checkUpdate(facilitieImage.Title, facilitieImage.Id))
            {
                return BadRequest("An Facilitie with the same title already exists.");
            }

            var result = await _facilitieRepo.UpdateFacilitie(facilitieImage);
            if (result != null)
            {
                return Ok(new
                {
                    message = "The facilitie has been updated successfully"
                });
            }
            return BadRequest("An error occurred while updating the facilitie");
        }
    }
  
}

