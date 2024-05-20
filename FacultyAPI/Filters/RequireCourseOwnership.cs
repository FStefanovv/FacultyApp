using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FacultyApp.Attributes;

public class RequireCourseOwnershipAttribute : ServiceFilterAttribute {
    public RequireCourseOwnershipAttribute() : base(typeof(RequireCourseOwnershipFilter)) { }
}

public class RequireCourseOwnershipFilter : IAuthorizationFilter
{
    private readonly ILogger<RequireCourseOwnershipFilter> _logger;
    private readonly StudentsDbContext _dbContext;

    public RequireCourseOwnershipFilter(ILogger<RequireCourseOwnershipFilter> logger,
                                        StudentsDbContext dbContext){
        _logger = logger;
        _dbContext = dbContext;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string? userId = context.HttpContext.Items["UserId"]?.ToString();

        if(String.IsNullOrEmpty(userId)){
            context.Result = new ForbidResult();
            return;
        }

        string? courseId = context.RouteData.Values["courseId"]?.ToString();

        if(!String.IsNullOrEmpty(courseId)) {
            if(!_dbContext.Courses.Any(c => c.Id == courseId && c.TeacherId == userId)){
                context.Result = new ForbidResult();
                return;
            } 
            else return;
        }

        string? examId = context.RouteData.Values["examId"]?.ToString();
        if(!String.IsNullOrEmpty(examId)){
            if(!_dbContext.Examinations.Any(e => e.Id == examId && e.TeacherId == userId)){
                context.Result = new ForbidResult();
                return;
            }
            else return;
        }

        context.Result = new BadRequestResult();
    }
}