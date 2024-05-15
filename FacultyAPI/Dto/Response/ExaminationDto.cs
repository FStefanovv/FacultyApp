namespace FacultyApp.Dto;

using FacultyApp.Enums;

public class ExaminationDto {

    public ExaminationDto(){}

    public string? Id {get; set;}
    public string? CourseName {get; set;}
    public DateTime? ScheduledFor {get; set;}
    public ExaminationStatus? Status {get; set;}
    public string? TeacherName {get; set;}
    public int? AvailablePlaces {get; set;}
}
