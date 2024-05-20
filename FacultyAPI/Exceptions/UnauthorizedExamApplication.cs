namespace FacultyApp.Exceptions;

public class UnauthorizedExamApplication : Exception {
    public UnauthorizedExamApplication(){}

    public UnauthorizedExamApplication(string message) : base(message) {}
}