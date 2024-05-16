namespace FacultyApp.Dto;

public class StudentDto {
    public string Id {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public DateTime DateOfBirth {get; set;}
    public string Email {get; set;}
    public int CurrentYear {get; set;}
    public uint EnrolledIn {get; set;}
    public bool Graduated {get; set;}
    public float GPA {get; set;}
}