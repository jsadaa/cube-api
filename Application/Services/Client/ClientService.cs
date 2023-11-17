using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.Exceptions;
using ApiCube.Domain.Enums.Administration;
using ApiCube.Persistence.Models;
using ApiCube.Persistence.Repositories.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.Client;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(
        IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<BaseResponse> AjouterUnClient(ClientRequest clientRequest)
    {
        try
        {
            var user = new ApplicationUserModel
            {
                UserName = clientRequest.Nom + clientRequest.Prenom,
                Email = clientRequest.Email,
                EmailConfirmed = true
            };

            var client = new Domain.Entities.Client(
                username: clientRequest.Nom + clientRequest.Prenom,
                nom: clientRequest.Nom,
                prenom: clientRequest.Prenom,
                adresse: clientRequest.Adresse,
                codePostal: clientRequest.CodePostal,
                ville: clientRequest.Ville,
                pays: clientRequest.Pays,
                telephone: clientRequest.Telephone,
                email: clientRequest.Email,
                dateNaissance: clientRequest.DateNaissance,
                dateInscription: DateTime.Now
            );

            await _clientRepository.Ajouter(client, user, clientRequest.Password);

            var response = new BaseResponse(
                HttpStatusCode.Created,
                new { code = "client_ajoute" }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = "client_existe_deja" }
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