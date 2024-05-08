using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FacultyApp.Dto;

public class RegisterTeacherDto : RegistrationDto {
    [Required(ErrorMessage = "Teacher department is required.")]
    public string Department { get; set; }

}