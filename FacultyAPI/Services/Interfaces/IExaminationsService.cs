namespace FacultyApp.Services.Interfaces;

using FacultyApp.Dto;
using FacultyApp.Model;

public interface IExaminationsService
{
    Task CancelExamination(string id);
    Task<Examination> CreateExamination(NewExaminationDto dto);
    Task<Examination?> GetById(string id);
    List<ExaminationDto> GetCourseExaminations(string courseId, string filter);
    List<Examination> GetTeacherExaminations(string userId, string filter); 
}