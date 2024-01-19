using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private IStaff service;
        public StaffController(IStaff service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<Staff> GetOneStaff(int id)
        {
            return await service.GetOneStaff(id);
        }

        [HttpGet("GetList")]
        public async Task<IEnumerable<Staff>> GetList()
        {
            return await service.GetAllStaffs();
        }

        [HttpPost]
        public async Task<Staff> PostCourse(Staff staff)
        {
            return await service.AddStaff(staff);
        }

        [HttpDelete("{id}")]
        public async Task<Staff> DeleteStaff(int id)
        {
            return await service.DeleteStaff(id);
        }

        [HttpPut]
        public async Task<Staff> PutStaff(Staff staff)
        {
            return await service.UpdateStaff(staff);
        }
    }
}
