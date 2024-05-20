namespace FacultyApp.Repository.Interfaces;

using FacultyApp.Model;

public interface IExaminationsRepository {
    Task SaveChangesAsync();
    Task Create(Examination examination);
    Task<Examination?> GetById(string id);
    List<Examination> GetTeacherExaminations(string teacherId, string filter);
    List<ExaminationApplication> GetStudentExaminations(string studentId, string filter);
    Task CreateApplication(ExaminationApplication examinationApplication);
}