using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacultyApp.Model;

public class Course {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id {get; set;}
    [Required]
    public string Name {get; set;}
    [Required]
    [Range(1, 5, ErrorMessage = "Value of year must be between 1 and 5")]
    public int Year {get; set;}
    [Required]
    public string Department {get; set;}
    [Required]
    public int EspbPoints {get; set;}
    [Required]
    public string TeacherId {get; set;}

    [ForeignKey("TeacherId")]
    public virtual Teacher Teacher {get; set;}
    
    public virtual List<Examination> Examinations {get; set;}
}