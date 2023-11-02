using System.Net;
using ApiCube.DTOs;
using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;
using ApiCube.Models;

namespace ApiCube.Services.StudentService;

public class StudentService : IStudentService
{
    private readonly ApiDbContext _context;
    
    public StudentService(ApiDbContext context)
    {
        _context = context;
    }

    public BaseResponse CreateStudent(CreateStudentRequest request)
    {
        BaseResponse response;
        
        try
        {
            StudentModel newStudent = new StudentModel
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                Email = request.Email,
                ContactNumber = request.ContactNumber
            };

            using (_context)
            {
                _context.Students.Add(newStudent);
                _context.SaveChanges();
            }
            
            response = new BaseResponse(
                statusCode : HttpStatusCode.Created,
                data : new { message = "Student created successfully" }
            );
            
            return response;
        }
        catch (Exception e)
        {
            response = new BaseResponse(
                statusCode : HttpStatusCode.InternalServerError,
                data : new { message = e.Message }
            );
            
            return response;
        }
    }
    
    public BaseResponse StudentList()
    {
        BaseResponse response;
        
        try
        {
            List<StudentDTO> students = new List<StudentDTO>();

            using (_context)
            {
                students.AddRange(_context.Students.Select(student => new StudentDTO
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Address = student.Address,
                    Email = student.Email,
                    ContactNumber = student.ContactNumber
                }));
            }
            
            response = new BaseResponse(
                statusCode : HttpStatusCode.OK,
                data : new { students }
            );
            
            return response;
        }
        catch (Exception e)
        {
            response = new BaseResponse(
                statusCode : HttpStatusCode.InternalServerError,
                data : new { message = "Internal server error : " + e.Message }
            );
            
            return response;
        }
    }
}