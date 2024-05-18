using FacultyApp.Dto;

namespace FacultyApp.Services;

public interface ITokenGenerator {
    string GenerateToken(JwtUserDataDto userData);
}