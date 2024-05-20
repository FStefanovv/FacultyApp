namespace FacultyApp.Dto;

public class StudentDto : UserDataDto {
    public int CurrentYear {get; set;}
    public uint EnrolledIn {get; set;}
    public bool Graduated {get; set;}
    public float GPA {get; set;}
}