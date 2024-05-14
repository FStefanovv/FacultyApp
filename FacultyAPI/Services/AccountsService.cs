using FacultyApp.Dto;
using FacultyApp.Repository;
using FacultyApp.Model;
using FacultyApp.Exceptions;

namespace FacultyApp.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FacultyApp.Utils;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

public class AccountsService : IAccountsService {
    private readonly IAccountsRepository _repository; 
    private readonly IConfiguration _configuration;

    public AccountsService(IAccountsRepository repository, IConfiguration configuration) {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<string> Authenticate(LoginDto loginDTO) {
        User user = await _repository.GetByEmail(loginDTO.Email);

        if(user == null)
            throw new NotFoundException("No registered user with the entered email");

        if(PasswordHasher.HashPassword(loginDTO.Password) != user.Password) 
            throw new WrongPasswordException("Wrong password");

        string role;

        if(user is Teacher) role = "Teacher";
        else role = "Student";

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuth:Secret"]));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: new List<Claim> {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            },
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        return tokenString;
    }

    public async Task<User> Register(RegistrationDto registrationDto){
        User newUser;
        
        if(registrationDto is RegisterStudentDto studentDto){
            var student = new Student
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                DateOfBirth = studentDto.DateOfBirth.Date, 
                Email = studentDto.Email,
                Password = PasswordHasher.HashPassword(studentDto.Password),
                CurrentYear = 1, 
                EnrolledIn = (uint)DateTime.Now.Year, 
                Graduated = false, 
                GPA = 0.0f 
            };

            newUser = student;
        }
        else {
            var teacherDto = (RegisterTeacherDto)registrationDto;
            var teacher = new Teacher {
                FirstName = teacherDto.FirstName,
                LastName = teacherDto.LastName,
                DateOfBirth = teacherDto.DateOfBirth.Date, 
                Email = teacherDto.Email,
                Password = PasswordHasher.HashPassword(teacherDto.Password),
                EmployedIn = (uint)DateTime.Now.Year,
                Department = teacherDto.Department
            };

            newUser = teacher;
        }
           
        try {
            await _repository.Create(newUser);
            return newUser;
        } catch(NpgsqlException ex){
            throw new Exception(ex.Message);
        } catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    public async Task<User> GetById(string id) {
        return await _repository.GetById(id);
    }
}