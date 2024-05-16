namespace FacultyApp.Controller;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using FacultyApp.Dto;
using FacultyApp.Services;
using FacultyApp.Model;
using FacultyApp.ApiKey;
using FacultyApp.Exceptions;
using FacultyApp.Utils;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

[ApiController]
[Route("accounts")]
public class AccountsController : ControllerBase {
    
    private readonly IAccountsService _service;
    private readonly ILogger<AccountsController> _logger;
    private readonly IMapper _mapper;

    public AccountsController(IAccountsService service, ILogger<AccountsController> logger,
                                IMapper mapper){
        _service = service;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("~/login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> Login([FromBody] LoginDto loginDto) {
        try {
            var token = await _service.Authenticate(loginDto);
            var (httpOnly, expiration) = AuthCookieUtils.GetOptions();

            Response.Cookies.Append("X-Access-Token", token, httpOnly);
            Response.Cookies.Append("X-Expiration-Cookie", expiration.Expires.ToString()!, expiration);
            
            return Ok();
        } catch(NotFoundException) {
            return NotFound();
        } catch(WrongPasswordException){
            return Unauthorized();
        }
    }

    [HttpPost]
    [Route("~/logout")]
    [ProducesResponseType(200)]
    [Authorize]
    public ActionResult Logout(){
        var (httpOnly, expiration) = AuthCookieUtils.GetInvalidationOptions();
        Response.Cookies.Append("X-Access-Token", "", httpOnly);
        Response.Cookies.Append("X-Expiration-Cookie", "", expiration);

        return Ok();
    }

    [HttpPost]
    [Route("student")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> RegisterStudent([FromBody] RegisterStudentDto registerStudentDto) {
        try {
            Student createdStudent = (Student)await _service.Register(registerStudentDto);
            return CreatedAtAction(nameof(GetById), new { id = createdStudent.Id}, createdStudent);
            
        } catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("teacher")]
    [AdminApiKey]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> RegisterTeacher([FromBody] RegisterTeacherDto registerTeacherDto) {
        try {
            Teacher createdTeacher = (Teacher)await _service.Register(registerTeacherDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTeacher.Id}, createdTeacher);
            
        } catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{id}")]
    [Route("~/user-data")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ResponseCache(Location=ResponseCacheLocation.Client, Duration=120)]
    public async Task<ActionResult> GetById(string? id){
        var routeTemplate = ControllerContext.ActionDescriptor.AttributeRouteInfo!.Template;

        if(routeTemplate == "user-data"){
            id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        if(id == null){
            return BadRequest("User id not available");
        }

        User? user  = await _service.GetById(id);

        if(user == null) 
            return NotFound("User with the provided id doesn't exists");

    

        if(user is Teacher) {
            var teacherDto = _mapper.Map<TeacherDto>((Teacher)user);
            return Ok(teacherDto);
        }
        else  {
            if(!User.Identity!.IsAuthenticated) return Unauthorized();
            string? userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if(userId != user.Id) return Forbid();

            var studentDto = _mapper.Map<StudentDto>((Student)user);
    
            return Ok(studentDto);
        }
    }
}