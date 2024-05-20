namespace FacultyApp.Attributes;

using FacultyApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

public class StudentExaminationApplicationAttribute : ServiceFilterAttribute {
    public StudentExaminationApplicationAttribute() : base(typeof(StudentExaminationApplicationFilter)) {}
}

public class StudentExaminationApplicationFilter : IAuthorizationFilter
{   
    private readonly StudentsDbContext _dbContext;

    public StudentExaminationApplicationFilter(StudentsDbContext dbContext){
        _dbContext = dbContext;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string? userId = context.HttpContext.Items["UserId"]?.ToString();

        if(String.IsNullOrEmpty(userId)){
            context.Result = new BadRequestResult();
            return;
        }

        string? examId = context.RouteData.Values["examId"]?.ToString();

        if(String.IsNullOrEmpty(examId)){
            context.Result = new BadRequestResult();
            return;
        }

        Student? student = _dbContext.Students.Where(s => s.Id == userId).Include(s => s.ExamApplications).FirstOrDefault();
        Examination? examination = _dbContext.Examinations.Where(e => e.Id == examId).Include(e => e.Course).FirstOrDefault();
        
        if(student == null) {
            context.Result = new BadRequestResult();
            return;
        }

         if(examination == null) {
            context.Result = new BadRequestResult();
            return;
        }

        bool meetsCourseYearRequirement = student.CurrentYear >= examination.Course.Year;

        bool hasPassedOrApplied = student.ExamApplications.Any(ea => (ea.CourseId == examination.CourseId && ea.Status == Enums.ExamApplicationStatus.PASSED) || ea.ExaminationId == examId);
        








    }
}

