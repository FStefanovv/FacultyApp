using FacultyApp.Exceptions;
using FacultyApp.Model;
using FacultyApp.Repository.Interfaces;
using FacultyApp.Enums;

namespace FacultyApp.Services.Implementations;

using FacultyApp.Services.Interfaces;

public class StudentExaminationsService : IStudentExaminationsService {

    private readonly IExaminationsRepository _examsRepository;
    private readonly IAccountsRepository _accountsRepository;

    public StudentExaminationsService(IExaminationsRepository examsRepository, IAccountsRepository accountsRepository) {
        _examsRepository = examsRepository;
        _accountsRepository = accountsRepository;
    }

    public async Task ApplyForExamination(string studentId, string examId){
        Student student = (Student?)await _accountsRepository.GetById(studentId) 
                                    ?? throw new NotFoundException("Student not found"); 
        Examination examination = await _examsRepository.GetById(examId) 
                                    ?? throw new NotFoundException("Examination not found");
        
        CheckExaminationApplicability(examination);
        CheckIfCanApply(student, examination);    

        ExaminationApplication examinationApplication = new ExaminationApplication {
                                                            StudentId = studentId,
                                                            ExaminationId = examination.Id,
                                                            CourseId = examination.CourseId,
                                                            AppliedOn = DateTime.UtcNow, 
                                                            Status = ExamApplicationStatus.APPLIED,
                                                            Graded = false
                                                        };
        examination.AvailablePlaces -= 1;

        await _examsRepository.CreateApplication(examinationApplication);
    }

    public List<ExaminationApplication> GetStudentExaminations(string studentId, string filter){
        List<ExaminationApplication> exams =  _examsRepository.GetStudentExaminations(studentId, filter);
        
        return exams;
    }

    private void CheckExaminationApplicability(Examination examination){
        if(examination.Status == ExaminationStatus.CANCELLED) throw new Exception("Examination has been cancelled");
        if(DateTime.UtcNow.AddDays(2) > examination.ScheduledFor) throw new Exception("Application deadline exceeded");
        if(examination.AvailablePlaces == 0) throw new Exception("No more available places"); 
    }


    private void CheckIfCanApply(Student student, Examination examination){
        if(student.CurrentYear < examination.Course.Year) throw new UnauthorizedExamApplication("Student not yet allowed to apply for examinations in this course");

        bool hasPassedCourse = student.ExamApplications.Any(ea => ea.CourseId == examination.CourseId && ea.Status == ExamApplicationStatus.PASSED);  
        if(hasPassedCourse) throw new Exception("Student has already passed this course");

        bool hasAlreadyApplied = student.ExamApplications.Any(ea => ea.ExaminationId == examination.Id && (ea.Status == ExamApplicationStatus.APPLIED || ea.Status == ExamApplicationStatus.CANCELLED));
        if(hasAlreadyApplied) throw new Exception("Student has already applied for this examination"); 
    }

}