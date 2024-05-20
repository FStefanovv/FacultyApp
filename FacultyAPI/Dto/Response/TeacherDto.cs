namespace FacultyApp.Dto;

public class TeacherDto : UserDataDto {
    public string Department { get; set; }
    public uint EmployedIn {get; set;}
}