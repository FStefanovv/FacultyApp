namespace FacultyApp.Controller;

using AutoMapper;
using FacultyApp.Dto;
using FacultyApp.Model;
using FacultyApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Authorize(Roles = "Student")]
[Route("student-exams")]
public class StudentExaminationsController : ControllerBase {
    
    private readonly IStudentExaminationsService _studentExamsService;
    private readonly IMapper _mapper;


    public StudentExaminationsController(IStudentExaminationsService studentExaminationsService,
                                        IMapper mapper){
        _studentExamsService = studentExaminationsService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("~/apply-exam/{examId}")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<ActionResult> ApplyForExamination(string examId){
        string? studentId = HttpContext.Items["UserId"]?.ToString();

        if(studentId == null) return BadRequest("No student id provided");
        
        try {
            await _studentExamsService.ApplyForExamination(studentId, examId);
            return Ok();
        }  catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{filter}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public ActionResult GetStudentExams(string filter){
        string? userId = (string?)HttpContext.Items["UserId"];
        
        if(userId == null)
            return BadRequest();
 
        List<ExaminationApplication> exams = _studentExamsService.GetStudentExaminations(userId, filter);   

        var examDtos = exams.Select(exam => _mapper.Map<ExaminationDto>(exam));
        
        return Ok(examDtos);
    }
}