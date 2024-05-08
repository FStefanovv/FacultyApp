namespace FacultyApp.Model;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


using System.ComponentModel;

public class User {
    public User(){}

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id {get; set;}
    [Required]
    public string FirstName {get; set;}
    [Required]
    public string LastName {get; set;}
    [Required]
    public DateTime DateOfBirth {get; set;}
    [Required]
    public string Email {get; set;}
    [Required]
    [PasswordPropertyText]
    public string Password {get; set;}
}