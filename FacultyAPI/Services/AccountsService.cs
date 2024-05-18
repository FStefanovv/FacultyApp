using FacultyApp.Dto;
using FacultyApp.Repository;
using FacultyApp.Model;
using FacultyApp.Exceptions;

namespace FacultyApp.Services;

using AutoMapper;
using FacultyApp.Utils;

public class AccountsService : IAccountsService {
    private readonly IAccountsRepository _repository; 
    private readonly IMapper _mapper;
    private readonly ITokenGenerator _tokenGenerator;

    public AccountsService(IAccountsRepository repository, IMapper mapper, ITokenGenerator tokenGenerator) {
        _repository = repository;
        _mapper = mapper;
        _tokenGenerator = tokenGenerator;
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

        var tokenString = _tokenGenerator.GenerateToken(jwtUserData);

        return tokenString;
    }

    public async Task<User> Register(RegistrationDto registrationDto){
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
            return newUser;
        } catch(Exception ex){
            throw new Exception(ex.Message);
        }
    }

    public async Task<User?> GetById(string id) {
        return await _repository.GetById(id);
    }
}