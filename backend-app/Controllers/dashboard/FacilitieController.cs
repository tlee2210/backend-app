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
            _facilitieRepo = facilitieRepo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _facilitieRepo.GetAll());

        }
        [HttpPost]
        public async Task<ActionResult> PostFacilitie(Facilities faci)
        {
            try
            {
                var result = await _facilitieRepo.AddFaciliti(faci);
                if (result)
                {
                    return Ok("Add Faci successfully");
                }
                return BadRequest("add faci fail");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> PutFacilitie(Facilities faci)
        {
            try
            {
                var result = await _facilitieRepo.UpdateFaciliti(faci);
                if (result)
                {
                    return Ok("Update success");
                }
                return BadRequest("Update fail");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
    }
}

