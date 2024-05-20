using FacultyApp.Enums;
using FacultyApp.Model;
using Microsoft.EntityFrameworkCore;
using FacultyApp.Repository.Interfaces;

namespace FacultyApp.Repository.Implementations;

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
                                && e.ScheduledFor > DateTime.UtcNow)
                    .OrderBy(e => e.ScheduledFor).ToList();
        } else {
            return _context.Examinations
                    .Include(e => e.Teacher)
                    .Include(e => e.Course)
                    .Where(e => e.TeacherId == teacherId).ToList();     
        }
    }

    public List<ExaminationApplication> GetStudentExaminations(string studentId, string filter){
        if(filter == "all")    
            return _context.ExamApplications.Where(ea => ea.StudentId == studentId).ToList();
        
        return new();
    }

    public async Task CreateApplication(ExaminationApplication examinationApplication){
        await _context.ExamApplications.AddAsync(examinationApplication);
        await _context.SaveChangesAsync();
    }
}

