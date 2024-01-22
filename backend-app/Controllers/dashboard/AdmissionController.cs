﻿using backend_app.IRepository.dashboard;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.dashboard
{
    [Route("api/dashboard/admission")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmission service;
        public AdmissionController(IAdmission service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<Admission> GetOneAdmission(int id)
        {
            return await service.GetOneAdmission(id);
        }

        [HttpGet("GetAllProccess")]
        public async Task<IEnumerable<Admission>> GetAllProccess()
        {
            return await service.GetAllProccess();
        }

        [HttpGet("GetAllAccept")]
        public async Task<IEnumerable<Admission>> GetAllAccept()
        {
            return await service.GetAllAccept();
        }

        [HttpGet("GetAllReject")]
        public async Task<IEnumerable<Admission>> GetAllReject()
        {
            return await service.GetAllReject();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Admission>> DeleteAdmission(int id)
        {
            var ad = await service.DeleteAdmission(id);
            if (ad != null)
            {
                return Ok(new
                {
                    message = "New Admission Deleted Successfully"
                });
            }
            return BadRequest("false");
        }

        [HttpPost("{id}/accept")]
        public async Task<ActionResult<Admission>> AcceptAdmission(int id)
        {
            var ad = await service.AcceptAdmission(id);
            if (ad != null)
            {
                return Ok(new
                {
                    message = "New Admission Update Successfully"
                });
            }
            return BadRequest("false");
        }

        [HttpPost("{id}/reject")]
        public async Task<ActionResult<Admission>> RejectAdmission(int id)
        {
            var ad = await service.RejectAdmission(id);
            if (ad != null)
            {
                return Ok(new
                {
                    message = "New Admission Update Successfully"
                });
            }
            return BadRequest("false");
        }
    }
}
