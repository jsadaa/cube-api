using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.Exceptions;
using ApiCube.Persistence.Models;
using ApiCube.Persistence.Repositories.Employe;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.Employe;

public class EmployeService : IEmployeService
{
    private readonly IEmployeRepository _employeRepository;

    public EmployeService(IEmployeRepository employeRepository)
    {
        _employeRepository = employeRepository;
    }

    public async Task<BaseResponse> AjouterUnEmploye(EmployeRequest employeRequest)
    {
        try
        {
            var user = new ApplicationUserModel
            {
                UserName = employeRequest.Nom + employeRequest.Prenom,
                Email = employeRequest.Email,
                EmailConfirmed = true
            };

            var employe = new Domain.Entities.Employe(
                nom: employeRequest.Nom,
                prenom: employeRequest.Prenom,
                email: employeRequest.Email,
                dateEmbauche: employeRequest.DateEmbauche,
                statut: employeRequest.Statut
            );

            await _employeRepository.Ajouter(employe, user, employeRequest.Password);

            var response = new BaseResponse(
                HttpStatusCode.Created,
                new { code = "employe_ajoute" }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                "employe_existe_deja"
            );

            return response;
        }
        catch (UtilisateurExisteDeja e)
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = e.Message }
            );

            return response;
        }
        catch (FormatMotDePasseInvalide e)
        {
            var response = new BaseResponse(
                HttpStatusCode.BadRequest,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }
}