namespace FacultyApp.Dto;

public class TeacherDto {
    public string Id {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public DateTime DateOfBirth {get; set;}
    public string Email {get; set;}
    public string Department { get; set; }
    public uint EmployedIn {get; set;}
}