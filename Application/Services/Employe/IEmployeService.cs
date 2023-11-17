using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.Employe;

public interface IEmployeService
{
    Task<BaseResponse> AjouterUnEmploye(EmployeRequest employeRequest);
}