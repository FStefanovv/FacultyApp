using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FacultyApp.Services.Interfaces;


[ApiController]
[Route("courses")]
public class CoursesController : ControllerBase {
    private readonly ICoursesService _service;
    private readonly IMapper _mapper;

    public CoursesController(IMapper mapper, ICoursesService service) {
        _service = service;
        _mapper = mapper;
    }

    
    [HttpGet]
    [Route("~/my-courses")]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> GetUserCourses(){
        string? userId = (string?)HttpContext.Items["UserId"];
        string? userRole = (string?)HttpContext.Items["UserRole"];
        
        if(userId == null || userRole == null)
            return BadRequest();

        var coursesDto = await _service.GetUserCourses(userId, userRole);

        return Ok(coursesDto);
    }
}