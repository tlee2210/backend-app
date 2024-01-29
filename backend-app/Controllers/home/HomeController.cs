using backend_app.DTO;
using backend_app.IRepository.dashboard;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.home
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IHome service;
        public HomeController(IHome service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("/GetAll")]
        public async Task<HomeDTO<Faculty, Article>> GetAll()
        {
            return await service.GetAll();
        }
    }
}
