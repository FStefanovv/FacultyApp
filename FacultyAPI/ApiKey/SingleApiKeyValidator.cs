namespace FacultyApp.ApiKey;

public class SingleApiKeyValidator : IApiKeyValidator {
    private readonly IConfiguration _configuration; 

    public SingleApiKeyValidator(IConfiguration configuration) {
        _configuration = configuration;
    }

    public bool IsValid(string apiKey){
        if(string.IsNullOrEmpty(apiKey)){
            return false;
        }
        
        string key = _configuration.GetValue<string>("ApiKey");
        if(apiKey != key) return false;
        else return true;
    }
}