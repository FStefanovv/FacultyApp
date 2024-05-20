using FacultyApp.Dto;

namespace FacultyApp.Services.Interfaces;

public interface ICoursesService {
    Task<List<CourseDto>> GetUserCourses(string userId, string userRole);
}