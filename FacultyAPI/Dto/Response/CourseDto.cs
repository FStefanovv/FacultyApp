namespace FacultyApp.Dto;

public class CourseDto {

    public CourseDto(){
    
    }

    public string? Id {get; set;}
    public string? Name {get; set;}
    public int? Year {get; set;}
    public string? Department {get; set;}
    public int? EspbPoints {get; set;}
    public string? Teacher {get; set;}
    public string? TeacherId {get; set;}
    public bool? Passed {get; set;}
    public int? Grade {get; set;}
}