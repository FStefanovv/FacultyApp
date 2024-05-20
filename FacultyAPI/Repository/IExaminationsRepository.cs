namespace FacultyApp.Repository;

using FacultyApp.Model;

public interface IExaminationsRepository {
    Task SaveChangesAsync();
    Task Create(Examination examination);
    Task<Examination?> GetById(string id);
    Task<List<Course>> GetCourses(string userId, string userRole);
    List<Examination> GetTeacherExaminations(string teacherId, string filter);
    List<ExaminationApplication> GetStudentExaminations(string studentId, string filter);
    Task CreateApplication(ExaminationApplication examinationApplication);
}