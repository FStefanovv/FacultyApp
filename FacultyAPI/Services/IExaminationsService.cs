namespace FacultyApp.Services;

using FacultyApp.Dto;
using FacultyApp.Model;

public interface IExaminationsService
{
    Task CancelExamination(string id);
    Task<Examination> CreateExamination(string teacherId, string courseId, DateTime scheduledFor);
    Task<Examination> GetById(string id);
    List<ExaminationDto> GetExaminations(string userId, string filter, bool isTeacher);
    Task<List<CourseDto>> GetTeacherCourses(string teacherId);
}