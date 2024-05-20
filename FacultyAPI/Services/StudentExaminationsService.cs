using FacultyApp.Exceptions;
using FacultyApp.Model;
using FacultyApp.Repository;
using FacultyApp.Enums;

namespace FacultyApp.Services;

public class StudentExaminationsService : IStudentExaminationsService {

    private readonly IExaminationsRepository _examsRepository;
    private readonly IAccountsRepository _accountsRepository;

    public StudentExaminationsService(IExaminationsRepository examsRepository, IAccountsRepository accountsRepository) {
        _examsRepository = examsRepository;
        _accountsRepository = accountsRepository;
    }

    public async Task ApplyForExamination(string studentId, string examId){

        Student? student = (Student?)await _accountsRepository.GetById(studentId); 
        if(student == null) throw new Exception("Student not found");

        Examination? examination = await _examsRepository.GetById(examId);
        if(examination == null) throw new NotFoundException("Examination not found");

        if(student.CurrentYear < examination.Course.Year) throw new UnauthorizedExamApplication("Student not yet allowed to apply for examinations in this course");

        if(examination.Status == ExaminationStatus.CANCELLED) throw new Exception("Examination has been cancelled");
        if(DateTime.UtcNow.AddDays(2) > examination.ScheduledFor) throw new Exception("Application deadline exceeded");
        if(examination.AvailablePlaces == 0) throw new Exception("No more available places"); 

        bool hasPassedCourse = student.ExamApplications.Any(ea => ea.CourseId == examination.CourseId && ea.Status == ExamApplicationStatus.PASSED);  
        if(hasPassedCourse) throw new Exception("Student has already passed this course");

        bool hasApplied = student.ExamApplications.Any(ea => ea.ExaminationId == examId && (ea.Status == ExamApplicationStatus.APPLIED || ea.Status == ExamApplicationStatus.CANCELLED));
        if(hasApplied) throw new Exception("Student has already applied for this examination"); 
        ExaminationApplication examinationApplication = new ExaminationApplication {
                                                            StudentId = studentId,
                                                            ExaminationId = examination.Id,
                                                            CourseId = examination.CourseId,
                                                            AppliedOn = DateTime.UtcNow, 
                                                            Graded = false
                                                        };


        await _examsRepository.CreateApplication(examinationApplication);
    }

}