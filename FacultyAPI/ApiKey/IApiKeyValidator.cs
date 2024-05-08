namespace FacultyApp.ApiKey;

public interface IApiKeyValidator {
    bool IsValid(string apiKey);
}