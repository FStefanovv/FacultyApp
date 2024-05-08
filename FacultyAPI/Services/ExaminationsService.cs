using FacultyApp.Dto;
using FacultyApp.Enums;
using FacultyApp.FireForget;
using FacultyApp.Model;
using FacultyApp.Repository;

namespace FacultyApp.Services;

public class ExaminationsService : IExaminationsService
{
    private readonly IExaminationsRepository _repository;
    private readonly NotificationFireForgetHandler _notificationHandler;

    public ExaminationsService(IExaminationsRepository repository,
                               NotificationFireForgetHandler notificationHandler){
        _repository = repository;
        _notificationHandler = notificationHandler;
    }
    
    public async Task<Examination> CreateExamination(NewExaminationDto dto){
        Examination examination = new Examination { 
                                    TeacherId = dto.TeacherId, ScheduledFor = dto.ScheduledFor.ToUniversalTime(), 
                                    Status = ExaminationStatus.SCHEDULED, CourseId = dto.CourseId,
                                    AvailablePlaces = dto.AvailablePlaces
                                };
        
        await _repository.Create(examination);

        _notificationHandler.NotifyStudentsAboutExamination(examination.Id, NotificationType.EXAMINATION_CREATED);

        return examination;
    }

    public async Task CancelExamination(string id){   
        var examination = await _repository.GetById(id);

        if(examination.Status != ExaminationStatus.SCHEDULED)
            throw new Exception("Examination has already been cancelled");

        if(DateTime.UtcNow.AddDays(2) >= examination.ScheduledFor)
            throw new Exception("Examinaton has to be cancelled at least 2 days in advance");
        
        examination.Status = ExaminationStatus.CANCELLED;
        _notificationHandler.NotifyStudentsAboutExamination(examination.Id, NotificationType.EXAMINATION_CANCELLED);
        await _repository.SaveChangesAsync();
    }

    public async Task<Examination> GetById(string id)
    {
        return await _repository.GetById(id);
    }

    public async Task<List<CourseDto>> GetTeacherCourses(string teacherId) {
        List<Course> teacherCourses = await _repository.GetTeacherCoursesEager(teacherId);
        List<CourseDto> courseDtos = new List<CourseDto>();
        foreach(Course course in teacherCourses) {
            var current = new CourseDto {
                Id = course.Id,
                Name = course.Name,
                Year = course.Year,
                Department = course.Department,
                EspbPoints = course.EspbPoints,
                Teacher = course.Teacher.FirstName + " " + course.Teacher.LastName,
                TeacherId = course.TeacherId
            };
           
            courseDtos.Add(current);
        }
        
        return courseDtos;
    }

    public List<ExaminationDto> GetExaminations(string userId, string filter, bool isTeacher){
        List<Examination> exams =  _repository.GetExaminations(userId, filter, isTeacher);
        List<ExaminationDto> examDtos = new ();
        foreach(Examination exam in exams){
            ExaminationDto currentDto = new ExaminationDto {
                Id = exam.Id,
                CourseName = exam.Course.Name,
                ScheduledFor = exam.ScheduledFor,
                Status = exam.Status,
                TeacherName = exam.Teacher.FirstName + " " + exam.Teacher.LastName,
                AvailablePlaces = exam.AvailablePlaces
            };
            examDtos.Add(currentDto);
        }
        return examDtos;
    }

   
}