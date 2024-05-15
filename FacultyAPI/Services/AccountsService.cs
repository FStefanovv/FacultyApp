using FacultyApp.Dto;
using FacultyApp.Repository;
using FacultyApp.Model;
using FacultyApp.Exceptions;

namespace FacultyApp.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FacultyApp.Utils;
using Microsoft.IdentityModel.Tokens;

public class AccountsService : IAccountsService {
    private readonly IAccountsRepository _repository; 
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AccountsService(IAccountsRepository repository, IConfiguration configuration,
                           IMapper mapper) {
        _repository = repository;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<string> Authenticate(LoginDto loginDTO) {
        User? user = await _repository.GetByEmail(loginDTO.Email);

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
            var student = _mapper.Map<Student>(studentDto);  
            newUser = student;
        }
        else {
            var teacherDto = (RegisterTeacherDto)registrationDto;
            var teacher = _mapper.Map<Teacher>(teacherDto);       
            newUser = teacher;
        }
           
        try {
            await _repository.Create(newUser);
            return newUser;
        } catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    public async Task<User?> GetById(string id) {
        return await _repository.GetById(id);
    }
}