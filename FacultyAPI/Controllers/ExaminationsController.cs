namespace FacultyApp.Controller;

using System.Security.Claims;
using FacultyApp.Dto;
using FacultyApp.Attributes;
using FacultyApp.Model;
using FacultyApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

[ApiController]
[Route("exams")]
public class ExaminationsController : ControllerBase {
    private readonly ITeacherExaminationsService _teacherService;
    private readonly IStudentExaminationsService _studentService;
    private readonly IMapper _mapper;
    private readonly ILogger<ExaminationsController> _logger;


    public ExaminationsController(ITeacherExaminationsService teacherService, 
                                  IStudentExaminationsService studentService,
                                  ILogger<ExaminationsController> logger,
                                  IMapper mapper) {
        _teacherService = teacherService;
        _studentService = studentService;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("{courseId}")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [Authorize(Roles = "Teacher")]
    [RequireCourseOwnership]
    public async Task<ActionResult> ScheduleExamination([FromRoute] string courseId, [FromBody] NewExaminationDto newExaminationDto) {
        string? teacherId = (string?)HttpContext.Items["UserId"];
        try {
            newExaminationDto.CourseId = courseId;
            newExaminationDto.TeacherId = teacherId;

            Examination newExamination = await _teacherService.CreateExamination(newExaminationDto);
            
            return CreatedAtAction(nameof(GetById), new {id = newExamination.Id}, newExamination);
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetById(string id)
    {
        Examination? exam = await _teacherService.GetById(id);

        if(exam == null) return NotFound();

        ExaminationDto dto = _mapper.Map<ExaminationDto>(exam);

        return Ok(dto);
    }

    [HttpDelete]
    [Route("{examId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [Authorize(Roles = "Teacher")]
    [RequireCourseOwnership]
    public async Task<ActionResult> CancelExamination(string examId) {
        try {
            await _teacherService.CancelExamination(examId);
            return NoContent();
        } catch(Exception ex){
            return BadRequest(ex.Message);
        }       
    }

    [HttpGet]
    [Route("~/my-courses")]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> GetCourses(){
        string? userId = (string?)HttpContext.Items["UserId"];
        string? userRole = (string?)HttpContext.Items["UserRole"];
        
        if(userId == null || userRole == null)
            return BadRequest();

        var courses = await _teacherService.GetCourses(userId, userRole);

        List<CourseDto> courseDtos = courses.Select(course => _mapper.Map<CourseDto>(course)).ToList();

        return Ok(courseDtos);
    }

    [HttpGet]
    [Route("filter/{filter}")]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public ActionResult GetExams(string filter){

        string? userId = (string?)HttpContext.Items["UserId"];
        string? userRole = (string?)HttpContext.Items["UserRole"];
        
        if(userId == null || userRole == null)
            return BadRequest();


        List<Examination> exams =  new();

        if(userRole == "Teacher")
            exams = _teacherService.GetTeacherExaminations(userId, filter);   
        else {

        }

        var examDtos = exams.Select(exam => _mapper.Map<ExaminationDto>(exam));

        return Ok(examDtos);
    }

    [HttpPost]
    [Route("apply/{examId}")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [Authorize(Roles = "Student")]
    [StudentExaminationApplication]
    public async Task<ActionResult> ApplyForExamination(string examId){
        string? studentId = HttpContext.Items["UserId"]?.ToString();

        if(studentId == null) return BadRequest("No student id provided");
        
        try {
            await _studentService.ApplyForExamination(studentId, examId);
            return Ok();
        }  catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }
}