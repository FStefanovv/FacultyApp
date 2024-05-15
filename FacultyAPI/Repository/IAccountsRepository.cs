namespace FacultyApp.Repository;

using FacultyApp.Model;

public interface IAccountsRepository {
    Task<User?> GetByEmail(string email);
    Task<User?> GetById(string id);
    Task Create(User user);
}