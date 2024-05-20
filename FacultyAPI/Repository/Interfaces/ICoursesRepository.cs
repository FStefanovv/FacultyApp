namespace FacultyApp.Repository.Interfaces;

using FacultyApp.Model;

public interface ICoursesRepository {
    Task<List<Course>> GetUserCourses(string userId, string userRole);
}