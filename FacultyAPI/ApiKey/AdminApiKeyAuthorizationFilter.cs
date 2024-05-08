using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FacultyApp.ApiKey;

public class AdminApiKeyAuthorizationFilter : IAuthorizationFilter
{
    private const string ApiKeyHeader = "AdminApiKey";
    private readonly IApiKeyValidator _apiKeyValidator;

    public AdminApiKeyAuthorizationFilter(IApiKeyValidator apiKeyValidator){
        _apiKeyValidator = apiKeyValidator;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string apiKeyHeaderValue = context.HttpContext.Request.Headers[ApiKeyHeader];

        if(string.IsNullOrEmpty(apiKeyHeaderValue)){
            context.Result = new UnauthorizedResult();
        }

        if(!_apiKeyValidator.IsValid(apiKeyHeaderValue)){
            context.Result = new UnauthorizedResult();
        }
    }
}