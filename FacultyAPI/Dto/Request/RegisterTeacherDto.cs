using System.ComponentModel.DataAnnotations;
using FacultyApp.Dto;

public class RegisterTeacherDto : RegistrationDto {
    public RegisterTeacherDto(){}
    
    [Required(ErrorMessage = "Teacher department is required.")]
    public string Department { get; set; }
}