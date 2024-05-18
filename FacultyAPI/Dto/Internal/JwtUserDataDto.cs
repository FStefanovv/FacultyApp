namespace FacultyApp.Dto;

public class JwtUserDataDto {
    public JwtUserDataDto(string id, string email, string role){
        Id = id;
        Email = email;
        Role = role;
    }

    public string Id {get; set;}
    public string Email {get; set;}
    public string Role {get; set;}
}