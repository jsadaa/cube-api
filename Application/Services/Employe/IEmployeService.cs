using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.Employe;

public interface IEmployeService
{
    public Task<BaseResponse> AjouterUnEmploye(EmployeRequest employeRequest);
    public BaseResponse ListerLesEmployes();
    public BaseResponse TrouverUnEmploye(int id);
    public Task<BaseResponse> ModifierUnEmploye(int id, EmployeRequest employeRequest);
    public Task<BaseResponse> SupprimerUnEmploye(int id);
    
}