namespace FacultyApp.Controller;

using FacultyApp.Dto;
using FacultyApp.Attributes;
using FacultyApp.Model;
using FacultyApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

[ApiController]
[Route("exams")]
public class ExaminationsController : ControllerBase {
    private readonly ITeacherExaminationsService _teacherService;
    private readonly IMapper _mapper;

    public ExaminationsController(ITeacherExaminationsService teacherService, 
                                  IMapper mapper) {
        _teacherService = teacherService;
        _mapper = mapper;
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
    [Route("~/teacher-exams/{filter}")]
    [Authorize(Roles = "Teacher")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public ActionResult GetTeacherExams(string filter){

        string? userId = (string?)HttpContext.Items["UserId"];
        
        if(userId == null)
            return BadRequest();
 
        List<Examination> exams = _teacherService.GetTeacherExaminations(userId, filter);   

        var examDtos = exams.Select(exam => _mapper.Map<ExaminationDto>(exam));

        return Ok(examDtos);
    }
}