using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using FacultyApp.Enums;

namespace FacultyApp.Model;

public class ExaminationApplication {
    
    [Required]
    public string StudentId {get; set;}
    [ForeignKey("StudentId")]
    public virtual Student Student {get; set;} 

    [Required]
    public string ExaminationId {get; set;}
    [ForeignKey("ExaminationId")]
    public virtual Examination Examination {get; set;}

    [Required]
    public string CourseId {get; set;}

    [Required]
    public DateTime AppliedOn {get; set;}
    
    [Required]
    public ExamApplicationStatus Status {get; set;}

    public bool? Graded {get; set;}
    public int? Grade {get; set;}
}