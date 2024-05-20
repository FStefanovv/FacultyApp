using FacultyApp.Model;

namespace FacultyApp.Services.Interfaces;

public interface IStudentExaminationsService {
    Task ApplyForExamination(string studentId, string examId);
    List<ExaminationApplication> GetStudentExaminations(string studentId, string filter);
}