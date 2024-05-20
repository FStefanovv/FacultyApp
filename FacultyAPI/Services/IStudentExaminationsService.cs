namespace FacultyApp.Services;

public interface IStudentExaminationsService {
    Task ApplyForExamination(string studentId, string examId);
}