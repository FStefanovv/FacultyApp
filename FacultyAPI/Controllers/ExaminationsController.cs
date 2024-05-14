namespace FacultyApp.Controller;

using System.Security.Claims;
using FacultyApp.Dto;
using FacultyApp.Filters;
using FacultyApp.Model;
using FacultyApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("exams")]
public class ExaminationsController : ControllerBase {
    private readonly IExaminationsService _service;
    private readonly ILogger<ExaminationsController> _logger;

    public ExaminationsController(IExaminationsService service, ILogger<ExaminationsController> logger) {
        _service = service;
        _logger = logger;
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
        Examination exam = await _service.GetById(id);

        if(exam == null) return NotFound();
        else return Ok(exam);
    }

    [HttpDelete]
    [Route("{examId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [Authorize(Roles = "Teacher")]
    [RequireCourseOwnership]
    public async Task<ActionResult> CancelExamination(string examId) {
        try {
            await _service.CancelExamination(examId);
            return Ok();
        } catch(Exception ex){
            return BadRequest(ex.Message);
        }       
    }

    [HttpGet]
    [Route("~/my-courses")]
    [Authorize(Roles = "Teacher")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> GetTeacherCourses(){
        var teacherId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var teacherCourses = await _service.GetTeacherCourses(teacherId);

        return Ok(teacherCourses.ToList());
    }

    [HttpGet]
    [Route("filter/{filter}")]
    [Authorize]
    [ProducesResponseType(200)]
    public ActionResult GetExams(string filter){
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        var examinations = _service.GetExaminations(userId, filter, role == "Teacher");
        
        return Ok(examinations);
    }
}