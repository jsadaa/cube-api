using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;
using ApiCube.Services.StudentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// THIS IS AN EXAMPLE OF A CONTROLLER CLASS
namespace ApiCube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        
        [HttpPost("save")]
        public IActionResult CreateStudent([FromBody] CreateStudentRequest request)
        {
            BaseResponse response = _studentService.CreateStudent(request);
            
            return StatusCode(response.StatusCode, response.Data);
        }
        
        [HttpGet("list")]
        public IActionResult StudentList()
        {
            BaseResponse response = _studentService.StudentList();
            
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
