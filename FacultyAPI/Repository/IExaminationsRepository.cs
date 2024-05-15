namespace FacultyApp.Repository;

using FacultyApp.Model;

public interface IExaminationsRepository {
    Task SaveChangesAsync();
    Task Create(Examination examination);
    Task<Examination?> GetById(string id);
    Task<List<Course>> GetCoursesEager(string userId, string userRole);
    List<Examination> GetTeacherExaminations(string teacherId, string filter);
}