namespace FacultyApp.Services;

using FacultyApp.Dto;
using FacultyApp.Model;
using FacultyApp.Responses;

public interface IAccountsService {
    Task<(string, LoginResponse)> Authenticate(LoginDto loginDto);
    Task<User> Register(RegistrationDto registrationDto);
    Task<User> GetById(string id);
}