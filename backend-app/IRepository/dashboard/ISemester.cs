﻿using backend_app.DTO;
using backend_app.Models;

namespace backend_app.IRepository.dashboard
{
    public interface ISemester
    {
        Task<AllSelectOptionsDTO> GetCteateSemester();
        Task<AllSelectOptionsDTO> GetParameters();
        Task<DepartmentSemesterSession> Store(DepartmentSemesterSession departmentSemesterSession);
        Task<bool> Exists(int facultyId, int sessionId, int departmentId);
        Task<List<List<DepartmentSemesterSession>>> DivideInto8Semesters(SearchParameters searchParameters);
        Task<List<DepartmentSemesterSession>> CreateSemesters(int facultyId, int semesterNumber, int[] departmentIds);
        Task<bool> Delete(int id);
    }
}
