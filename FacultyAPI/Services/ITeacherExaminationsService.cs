namespace FacultyApp.Services;

using FacultyApp.Dto;
using FacultyApp.Model;

public interface ITeacherExaminationsService
{
    Task CancelExamination(string id);
    Task<Examination> CreateExamination(NewExaminationDto dto);
    Task<Examination?> GetById(string id);
     List<Examination> GetTeacherExaminations(string userId, string filter); 
     Task<List<Course>> GetCourses(string userId, string userRole);
}