using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacultyApp.Model;

public class Teacher : User {
    [Required]
    public string Department { get; set; }
    [Required]
    public uint EmployedIn {get; set;}

    public virtual List<Course> Courses {get; set;}
}