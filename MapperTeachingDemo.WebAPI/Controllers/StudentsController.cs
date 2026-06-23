using MapperTeachingDemo.Domain.Students;
using MapperTeachingDemo.Domain.Students.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MapperTeachingDemo.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IstudentService _studentService;

    public StudentsController(IstudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost]
    public async Task<ActionResult<AddStudentResultDto>> CreateWithManualMapping(CreateStudentDto dto, CancellationToken cancellation)
    {
        var result = await _studentService.CreateWithManualMappAsync(dto, cancellation);
        return Ok(result);
    }
    [HttpPost("Version2")]
    public async Task<ActionResult<AddStudentResultDto>> CreateWithMapping(CreateStudentDto dto, CancellationToken cancellation)
    {
       
        var result = await _studentService.CreateAsync(dto, cancellation);

        return Ok(result);
    }
    [HttpGet]
    public async Task<ActionResult<List<StudentDetailListDto>>> GetAll()
    {
        var result = await _studentService.GetAllAsync();
        return Ok(result);
    } 


}
