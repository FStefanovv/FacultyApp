using FacultyApp.Enums;
using FacultyApp.Model;
using Microsoft.EntityFrameworkCore;

namespace FacultyApp.Repository;

public class ExaminationsRepository : IExaminationsRepository {
    private readonly StudentsDbContext _context;

    private readonly ILogger<ExaminationsRepository> _logger;

    public ExaminationsRepository(StudentsDbContext context, ILogger<ExaminationsRepository> logger) {
        _context = context;
        _logger = logger;
    }

    public async Task Create(Examination examination)
    {
        await _context.Examinations.AddAsync(examination);
        await _context.SaveChangesAsync();
    }

    public async Task<Examination?> GetById(string id)
    {
        Examination? examination = await _context.Examinations.FirstOrDefaultAsync(e => e.Id == id);
        return examination;
    }

    public async Task SaveChangesAsync(){
        await _context.SaveChangesAsync();
    }

    public List<Examination> GetTeacherExaminations(string teacherId, string filter){        
        if(filter == "scheduled"){    
            return  _context.Examinations
                    .Include(e => e.Teacher)
                    .Include(e => e.Course)
                    .Where(e => e.TeacherId == teacherId && e.Status == ExaminationStatus.SCHEDULED 
                                && e.ScheduledFor > DateTime.UtcNow).ToList();
           
        } else {
            return _context.Examinations
                    .Include(e => e.Teacher)
                    .Include(e => e.Course)
                    .Where(e => e.TeacherId == teacherId).ToList();     
        }
    }

    public async Task<List<Course>> GetCourses(string userId, string userRole)
    {
        if(userRole == "Teacher")
            return _context.Courses.Where(c => c.TeacherId == userId).ToList();
        else {
            Student student =  await _context.Students.FirstOrDefaultAsync(u => userId == u.Id) ?? throw new Exception("No user with provided id");
            return _context.Courses.Where(c => c.Year <= student.CurrentYear).ToList();
        }
    }
}