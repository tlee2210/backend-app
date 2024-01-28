using backend_app.DTO;
using backend_app.IRepository.dashboard;
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
    }
}
