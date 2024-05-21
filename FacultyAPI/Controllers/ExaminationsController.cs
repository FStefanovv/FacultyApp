namespace FacultyApp.Controller;

using FacultyApp.Dto;
using FacultyApp.Attributes;
using FacultyApp.Model;
using FacultyApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FacultyApp.Exceptions;

[ApiController]
[Route("exams")]
public class ExaminationsController : ControllerBase {
    private readonly IExaminationsService _service;
    private readonly IMapper _mapper;

    public ExaminationsController(IExaminationsService service, 
                                  IMapper mapper) {
        _service = service;
        _mapper = mapper;
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

            Examination newExamination = await _service.CreateExamination(newExaminationDto);
            
            return CreatedAtAction(nameof(GetById), new {id = newExamination.Id}, newExamination);
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
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
    [Route("~/teacher-exams/{filter}")]
    [Authorize(Roles = "Teacher")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public ActionResult GetTeacherExams(string filter){

        string? userId = (string?)HttpContext.Items["UserId"];
        
        if(userId == null)
            return BadRequest();
 
        List<Examination> exams = _service.GetTeacherExaminations(userId, filter);   

        var examDtos = exams.Select(exam => _mapper.Map<ExaminationDto>(exam));

        return Ok(examDtos);
    }


    [HttpGet]
    [Route("~/course-exams/{courseId}/{filter}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult GetCourseExaminations(string courseId, string? filter){
        try {
            if(String.IsNullOrEmpty(filter))
                filter = "scheduled";
            var exams = _service.GetCourseExaminations(courseId, filter);

            return Ok(exams);

        } catch(NotFoundException){
            return NotFound("Non-existant course");
        }
    }
}