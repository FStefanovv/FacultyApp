namespace FacultyApp.Services.Interfaces;

using FacultyApp.Dto;
using FacultyApp.Model;

public interface IAccountsService {
    Task<string> Authenticate(LoginDto loginDto);
    Task<string> Register(RegistrationDto registrationDto);
    Task<UserDataDto?> GetById(string id);
}