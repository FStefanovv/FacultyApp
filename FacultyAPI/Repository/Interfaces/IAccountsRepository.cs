using System.Xml;

namespace FacultyApp.Repository.Interfaces;

using FacultyApp.Model;

public interface IAccountsRepository {
    Task<User?> GetByEmail(string email);
    Task<User?> GetById(string id);
    Task Create(User user);
}