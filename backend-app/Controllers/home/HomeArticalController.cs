using backend_app.IRepository.home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.home
{
    [Route("api/home/homeartical")]
    [ApiController]
    public class HomeArticalController : ControllerBase
    {
        private readonly IHomeArtical homeArtical;
        public HomeArticalController(IHomeArtical homeArtical)
        {
            this.homeArtical = homeArtical;
        }
        [HttpGet]
        public async Task<IActionResult> GetListArtical()
        {
            var artical = await homeArtical.GetList();
            if(artical != null)
            {
                return Ok(artical);
            }
         return NotFound();
        }
            
    }
}
