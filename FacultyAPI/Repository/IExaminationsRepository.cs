namespace FacultyApp.Repository;

using FacultyApp.Model;

public interface IExaminationsRepository {
    Task SaveChangesAsync();
    Task Create(Examination examination);
    Task<Examination> GetById(string id);
    Task<List<Course>> GetTeacherCoursesEager(string teacherId);
    List<Examination> GetExaminations(string userId, string filter, bool isTeacher);
}