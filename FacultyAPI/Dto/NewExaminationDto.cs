using System.ComponentModel.DataAnnotations;

namespace FacultyApp.Dto;

public class NewExaminationDto {
    public NewExaminationDto(){}

    [Required]
    public DateTime ScheduledFor {get; set;}
    [Required]
    public int AvailablePlaces {get; set;}
    public string TeacherId {get; set;}
    public string CourseId {get; set;}
}