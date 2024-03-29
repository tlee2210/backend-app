﻿using backend_app.IRepository.dashboard;
using backend_app.IRepository.home;
using backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers.home
{
    [Route("api/home/courses")]
    [ApiController]
    public class CoursesHomeController : ControllerBase
    {
        private ICoursesHome service;
        public CoursesHomeController(ICoursesHome services)
        {
            this.service = services;
        }

        [HttpGet("GetList")]
        public async Task<IEnumerable<Courses>> GetList()
        {
            return await service.GetAllCourses();
        }
        [HttpGet("GetFacultyByCourseSlug/{courseSlug}")]
        public async Task<IActionResult> GetFacultyByCourseSlug(string courseSlug)
        {
            var courseWithFaculty = await service.GetFacultyByCourseSlug(courseSlug);

            if (courseWithFaculty != null)
            {
                return Ok(courseWithFaculty);
            }
            else
            {
                return NotFound($"Course not found for the slug: {courseSlug}");
            }
        }
        [HttpGet("SearchFacultyByTitle/{Title}")]
        public async Task<IActionResult> SearchFacultyByTitle(string Title)
        {
            var faculties = await service.SearchFacultyByTitle(Title);

            return Ok(faculties);
        }
        [HttpGet("GetFacultyDetails/{facultySlug}")]
        public async Task<IActionResult> GetFacultyDetails(string facultySlug)
        {
            try
            {
                var facultyDetails = await service.GetFacultyDetails(facultySlug);

                if (facultyDetails != null)
                {
                        return Ok(facultyDetails);
                }
                else
                {
                    return NotFound($"Faculty details not found for the slug: {facultySlug}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving faculty details");
            }
        }

        [HttpGet("GetDepartmentDetails/{id}")]
        public async Task<IActionResult> GetDepartmentDetails(int id)
        {
            try
            {
                var DepartmentDetails = await service.GetDepartmentDetails(id);

                if (DepartmentDetails != null)
                {
                    return Ok(DepartmentDetails);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving faculty details");
            }
        }
    }
}
