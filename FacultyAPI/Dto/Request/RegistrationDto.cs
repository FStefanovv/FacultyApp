namespace FacultyApp.Dto;

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public abstract class RegistrationDto {
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Date of birth is required.")]
    public DateTime DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [PasswordPropertyText]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm password is required.")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [PasswordPropertyText]
    public string ConfirmPassword { get; set; }
}