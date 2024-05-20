namespace FacultyApp.Services.Implementations;

using FacultyApp.Dto;
using FacultyApp.Model;
using FacultyApp.Services.Interfaces;
using FacultyApp.Repository.Interfaces;
using AutoMapper;
using FacultyApp.Exceptions;

public class CoursesService : ICoursesService {
    private readonly ICoursesRepository _coursesRepository;
    private readonly IAccountsRepository _accountsRepository;
    private readonly IMapper _mapper;

    public CoursesService(ICoursesRepository coursesRepository, IMapper mapper, IAccountsRepository accountsRepository){
        _coursesRepository = coursesRepository;
        _mapper = mapper;
        _accountsRepository = accountsRepository;
    }

    public async Task<List<CourseDto>> GetUserCourses(string userId, string userRole) {
        List<Course> courses = await _coursesRepository.GetUserCourses(userId, userRole);
       
        if(userRole == "Teacher")
            return courses.Select(course => _mapper.Map<CourseDto>(course)).ToList();
        else {
            Student? student = (Student?)await _accountsRepository.GetById(userId);

            if(student == null) throw new NotFoundException("Student with provided id not found");

            return courses.Select(course => _mapper.Map<CourseDto>(course))
                          .Select(courseDto => SetStudentCourseStats(student, courseDto)).ToList();
        }
            
    }

    private CourseDto SetStudentCourseStats(Student student, CourseDto course){
        ExaminationApplication? application = student.ExamApplications.FirstOrDefault(ea => ea.CourseId == course.Id && ea.Graded == true  && ea.Status == Enums.ExamApplicationStatus.PASSED);

        if(application == null){
            course.Passed = false;
            course.Grade = -1;
        }

        else {
            course.Passed = true;
            course.Grade = application.Grade;
        }  

        return course;
    }
}
