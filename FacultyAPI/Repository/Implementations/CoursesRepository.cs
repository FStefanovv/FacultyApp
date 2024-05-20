namespace FacultyApp.Repository.Implementations;

using FacultyApp.Repository.Interfaces;
using FacultyApp;
using FacultyApp.Model;
using Microsoft.EntityFrameworkCore;

public class CoursesRepository : ICoursesRepository {
    private readonly StudentsDbContext _context;

    public CoursesRepository(StudentsDbContext context){
        _context = context;
    }

    public async Task<List<Course>> GetUserCourses(string userId, string userRole)
    {
        if(userRole == "Teacher")
            return _context.Courses.Where(c => c.TeacherId == userId).OrderBy(c => c.Year).ToList();
        else {
            Student student =  await _context.Students.FirstOrDefaultAsync(u => userId == u.Id) ?? throw new Exception("No user with provided id");
            return _context.Courses.Where(c => c.Year <= student.CurrentYear).OrderBy(c => c.Year).ToList();
        }
    }
}