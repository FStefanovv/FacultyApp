namespace FacultyApp.Services;

using FacultyApp.Dto;
using FacultyApp.Model;

public interface IAccountsService {
    Task<string> Authenticate(LoginDto loginDto);
    Task<User> Register(RegistrationDto registrationDto);
    Task<User?> GetById(string id);
}