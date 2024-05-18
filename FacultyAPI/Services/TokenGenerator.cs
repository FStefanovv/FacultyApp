namespace FacultyApp.Services;

using FacultyApp.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;



public class TokenGenerator : ITokenGenerator {

    private readonly IConfiguration _configuration;

    public TokenGenerator(IConfiguration configuration) {
        _configuration = configuration;
    }


    public string GenerateToken(JwtUserDataDto userData){
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuth:Secret"]));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: new List<Claim> {
                new Claim(ClaimTypes.Role, userData.Role),
                new Claim(ClaimTypes.Email, userData.Email),
                new Claim(ClaimTypes.NameIdentifier, userData.Id)
            },
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        return tokenString;
    }
}