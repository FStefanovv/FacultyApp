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
    private readonly IExaminationsService _service;
    private readonly ILogger<ExaminationsController> _logger;

    private readonly IMapper _mapper;

    public ExaminationsController(IExaminationsService service, 
                                  ILogger<ExaminationsController> logger,
                                  IMapper mapper) {
        _service = service;
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
        var teacherId =  User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        try {
            newExaminationDto.CourseId = courseId;
            newExaminationDto.TeacherId = teacherId;

            Examination newExamination = await _service.CreateExamination(newExaminationDto);
            
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
        Examination? exam = await _service.GetById(id);

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
            await _service.CancelExamination(examId);
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
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        
        if(userId == null || userRole == null)
            return BadRequest();

        var courses = await _service.GetCourses(userId, userRole);

        List<CourseDto> courseDtos = courses.Select(course => _mapper.Map<CourseDto>(course)).ToList();

        return Ok(courseDtos);
    }

    [HttpGet]
    [Route("filter/{filter}")]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public ActionResult GetExams(string filter){

        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        
        if(userId == null || userRole == null)
            return BadRequest();


        List<Examination> exams =  new();

        if(userRole == "Teacher")
            exams = _service.GetTeacherExaminations(userId, filter);   
        else {

        }

        var examDtos = exams.Select(exam => _mapper.Map<ExaminationDto>(exam));

        return Ok(examDtos);
    }
}