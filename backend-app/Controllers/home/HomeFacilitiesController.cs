using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.home
{
    [Route("api/home/facilities")]
    [ApiController]
    public class HomeFacilitiesController : ControllerBase
    {
        private readonly IHomeFacilities homeFacilities;
        public HomeFacilitiesController(IHomeFacilities homeFacilities)
        {
            this.homeFacilities = homeFacilities;
        }
        [HttpGet]
        public async Task<ActionResult<Facilities>> GetFacilitie()
        {
            var result = await homeFacilities.GetList();
            return Ok(result);
        }
    }
}
