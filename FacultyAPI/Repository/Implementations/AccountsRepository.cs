using FacultyApp.Model;
using Microsoft.EntityFrameworkCore;

namespace FacultyApp.Repository.Implementations;

using FacultyApp.Repository.Interfaces;

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
        User? user = await _context.Students.Where(s => s.Id == id).Include(s => s.ExamApplications).FirstOrDefaultAsync();
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