using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Services.StudentService;

public interface IStudentService
{
    BaseResponse CreateStudent(CreateStudentRequest request);
    
    BaseResponse StudentList();
}