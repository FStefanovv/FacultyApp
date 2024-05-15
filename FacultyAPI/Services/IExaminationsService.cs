namespace FacultyApp.Services;

using FacultyApp.Dto;
using FacultyApp.Model;

public interface IExaminationsService
{
    Task CancelExamination(string id);
    Task<Examination> CreateExamination(NewExaminationDto dto);
    Task<Examination?> GetById(string id);
     List<ExaminationDto> GetTeacherExaminations(string userId, string filter); 
     Task<List<CourseDto>> GetCourses(string userId, string userRole);
}