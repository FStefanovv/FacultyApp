using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FacultyApp.Enums;

namespace FacultyApp.Model;

public class Examination {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id {get; set;}

    [Required]
    public DateTime ScheduledFor {get; set;}

    [Required]
    public ExaminationStatus Status {get; set;}

    [Required]
    public string CourseId {get; set;}
   
    [ForeignKey("CourseId")]
    public virtual Course Course {get; set;}

    [Required]
    public string TeacherId {get; set;}
   
    [ForeignKey("TeacherId")]
    public virtual Teacher Teacher { get; set; }
}