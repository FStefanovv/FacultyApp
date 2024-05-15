using FacultyApp.Model;
using Microsoft.EntityFrameworkCore;

namespace FacultyApp.Repository;


public class AccountsRepository : IAccountsRepository {
    private readonly StudentsDbContext _context;

    public AccountsRepository(StudentsDbContext context) {
        _context = context;
    }

    public async Task<User?> GetByEmail(string email) {
        User? user = await _context.Students.FirstOrDefaultAsync(s => s.Email == email);
        if(user != null) return user;
        user = await _context.Teachers.FirstOrDefaultAsync(t => t.Email == email);
        if(user != null) return user;
 
        return null;
    }

    public async Task<User?> GetById(string id){
        User? user = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if(user != null) return user;
        user = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
        if(user != null) return user;

        return null;
    }



    public async Task Create(User user){
        
        if(user is Student student)
            await _context.Students.AddAsync(student);
        else if(user is Teacher teacher)
            await _context.Teachers.AddAsync(teacher);
        await _context.SaveChangesAsync();
    }



}