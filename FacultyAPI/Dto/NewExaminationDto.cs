using System.ComponentModel.DataAnnotations;

namespace FacultyApp.Dto;

public class NewExaminationDto {
    public NewExaminationDto(){}

    [Required]
    public DateTime ScheduledFor {get; set;}
}