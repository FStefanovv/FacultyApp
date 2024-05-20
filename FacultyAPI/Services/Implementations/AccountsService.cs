using FacultyApp.Dto;
using FacultyApp.Repository.Interfaces;
using FacultyApp.Model;
using FacultyApp.Exceptions;

namespace FacultyApp.Services.Implementations;

using FacultyApp.Services.Interfaces;

using AutoMapper;
using FacultyApp.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AccountsService : IAccountsService {
    private readonly IAccountsRepository _repository; 
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AccountsService(IAccountsRepository repository, IMapper mapper, IConfiguration configuration) {
        _repository = repository;
        _mapper = mapper;
        _configuration = configuration;
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

        var jwtUserData = new JwtUserDataDto(user.Id, user.Email, role);

        var tokenString = GenerateToken(jwtUserData);

        return tokenString;
    }

    public async Task<string> Register(RegistrationDto registrationDto){
        User newUser;
        
        if(registrationDto is RegisterStudentDto studentDto){
            newUser = _mapper.Map<Student>(studentDto);  
        }
        else {
            var teacherDto = (RegisterTeacherDto)registrationDto;
            newUser = _mapper.Map<Teacher>(teacherDto);       
        }
           
        try {
            await _repository.Create(newUser);
            return newUser.Id;
        } catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    public async Task<UserDataDto?> GetById(string id) {
        var user = await _repository.GetById(id);

        if(user == null) return null;

        if(user is Teacher teacher) {
            return _mapper.Map<TeacherDto>(teacher);
        } else {
            var student = (Student)user;
            return _mapper.Map<StudentDto>(student);
        }
    }

     
    private string GenerateToken(JwtUserDataDto userData){
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