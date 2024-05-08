using Microsoft.AspNetCore.Mvc;

namespace FacultyApp.ApiKey;

public class AdminApiKeyAttribute : ServiceFilterAttribute {
    public AdminApiKeyAttribute() : base(typeof(AdminApiKeyAuthorizationFilter)){}
}