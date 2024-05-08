using FacultyApp.Enums;
using FacultyApp.Model;
using Microsoft.EntityFrameworkCore;

namespace FacultyApp.Repository;

public class ExaminationsRepository : IExaminationsRepository {
    private readonly StudentsDbContext _context;

    public ExaminationsRepository(StudentsDbContext context) {
        _context = context;
    }

    public async Task Create(Examination examination)
    {
        await _context.Examinations.AddAsync(examination);
        await _context.SaveChangesAsync();
    }

    public async Task<Examination> GetById(string id)
    {
        return await _context.Examinations.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task SaveChangesAsync(){
        await _context.SaveChangesAsync();
    }

    public async Task<List<Course>> GetTeacherCoursesEager(string teacherId){
        var courses = await _context.Courses.Include(c => c.Teacher)
                                    .Where(c => c.TeacherId == teacherId).ToListAsync();

        return courses;
    }

    public List<Examination> GetExaminations(string userId, string filter, bool isTeacher){
        if(filter == "scheduled"){
            if(isTeacher)
                return  _context.Examinations
                        .Include(e => e.Teacher)
                        .Include(e => e.Course)
                        .Where(e => e.TeacherId == userId && e.Status == ExaminationStatus.SCHEDULED 
                                    && e.ScheduledFor > DateTime.UtcNow).ToList();
            //else handle student case
             
            return null;
        } else {
            if(isTeacher) 
                return _context.Examinations
                        .Include(e => e.Teacher)
                        .Include(e => e.Course)
                        .Where(e => e.TeacherId == userId).ToList();
            //else handle student case
            return null;
        }
    }



}