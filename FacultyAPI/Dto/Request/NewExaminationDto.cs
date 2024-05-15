using System.ComponentModel.DataAnnotations;
using FacultyApp.Attributes;

namespace FacultyApp.Dto;

public class NewExaminationDto {
    public NewExaminationDto(){}

    [Required(ErrorMessage = "Date needs to be specified")]
    [FutureDate]
    public DateTime ScheduledFor {get; set;}

    [Required(ErrorMessage = "Available places need to be specified")]
    [Range(1, int.MaxValue, ErrorMessage = "Minimum value for available places is 1")]
    public int AvailablePlaces {get; set;}

    public string? TeacherId {get; set;}
    public string? CourseId {get; set;}
}