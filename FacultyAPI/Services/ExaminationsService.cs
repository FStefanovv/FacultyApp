using AutoMapper;
using FacultyApp.Dto;
using FacultyApp.Enums;
using FacultyApp.Notifications;
using FacultyApp.Model;
using FacultyApp.Repository;
using FacultyApp.Exceptions;

namespace FacultyApp.Services;

public class ExaminationsService : IExaminationsService
{
    private readonly IExaminationsRepository _repository;
    private readonly NotificationsService _notificationsService;
    private readonly IMapper _mapper;

    public ExaminationsService(IExaminationsRepository repository,
                               NotificationsService notificationsService,
                               IMapper mapper){
        _repository = repository;
        _notificationsService =notificationsService;
        _mapper = mapper;
    }
    
    public async Task<Examination> CreateExamination(NewExaminationDto dto){
        Examination examination = _mapper.Map<Examination>(dto);

        await _repository.Create(examination);

        _notificationsService.NotifyStudentsAboutExamination(examination.Id, NotificationType.EXAMINATION_CREATED);

        return examination;
    }

    public async Task CancelExamination(string id){   
        var examination = await _repository.GetById(id) ?? throw new NotFoundException();

        if(examination.Status != ExaminationStatus.SCHEDULED)
            throw new Exception("Examination has already been cancelled");

        if(DateTime.UtcNow.AddDays(2) >= examination.ScheduledFor)
            throw new Exception("Examinaton has to be cancelled at least 2 days in advance");
        
        examination.Status = ExaminationStatus.CANCELLED;
        _notificationsService.NotifyStudentsAboutExamination(examination.Id, NotificationType.EXAMINATION_CANCELLED);
        await _repository.SaveChangesAsync();
    }

    public async Task<Examination?> GetById(string id)
    {
        return await _repository.GetById(id);
    }

    public async Task<List<Course>> GetCourses(string userId, string userRole) {
        List<Course> teacherCourses = await _repository.GetCourses(userId, userRole);
       
        return teacherCourses;
    }

    public List<Examination> GetTeacherExaminations(string userId, string filter){
        List<Examination> exams =  _repository.GetTeacherExaminations(userId, filter);
        
        return exams;
    }
}