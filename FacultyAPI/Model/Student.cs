using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacultyApp.Model;

public class Student : User {
    [Required]
    [Range(1, 5, ErrorMessage = "Current year must be between 1 and 5")]
    public int CurrentYear {get; set;}
    [Required]
    [Range(2010, 2030, ErrorMessage = "Minimum enrollment year is 2010")]
    public uint EnrolledIn {get; set;}
    [Required]
    public bool Graduated {get; set;}
    
    [Range(6.0, 10.0, ErrorMessage = "GPA must have values between 6.00 and 10.00")]
    public float GPA {get; set;}

}