using System.Security.Claims;

namespace FacultyApp.Middleware;


public class UserClaimsMiddleware {
    private readonly RequestDelegate _next;

    public UserClaimsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context){
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userEmail = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var userRole = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userId != null) context.Items["UserId"] = userId;
        if(userEmail != null) context.Items["UserEmail"] = userEmail;
        if (userRole != null) context.Items["UserRole"] = userRole;


        await _next(context);
    }
}