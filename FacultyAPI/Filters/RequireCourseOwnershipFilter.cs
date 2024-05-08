using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FacultyApp.Filters;

public class RequireCourseOwnershipAttribute : ServiceFilterAttribute {
    public RequireCourseOwnershipAttribute() : base(typeof(RequireCourseOwnershipFilter)) { }
}

public class RequireCourseOwnershipFilter : IAuthorizationFilter
{
    private readonly ILogger<RequireCourseOwnershipFilter> _logger;
    private readonly StudentsDbContext _context;

    public RequireCourseOwnershipFilter(ILogger<RequireCourseOwnershipFilter> logger,
                                        StudentsDbContext context){
        _logger = logger;
        _context = context;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string userId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;        
        string courseId = context.RouteData.Values["courseId"]?.ToString();

        if(!String.IsNullOrEmpty(courseId)) {
            if(!_context.Courses.Any(c => c.Id == courseId && c.TeacherId == userId)){
                context.Result = new ForbidResult();
                return;
            } 
            else return;
        }

        string examId = context.RouteData.Values["examId"]?.ToString();
        if(!String.IsNullOrEmpty(examId)){
            if(!_context.Examinations.Any(e => e.Id == examId && e.TeacherId == userId)){
                context.Result = new ForbidResult();
                return;
            }
            else return;
        }
        context.Result = new BadRequestResult();
    }
}