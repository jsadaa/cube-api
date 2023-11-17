using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Domain.Enums.Administration;
using ApiCube.Persistence.Models;
using ApiCube.Persistence.Repositories.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.Client;

public class ClientService : IClientService
{
    private readonly UserManager<ApplicationUserModel> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IClientRepository _clientRepository;

    public ClientService(UserManager<ApplicationUserModel> userManager, IConfiguration configuration,
        IClientRepository clientRepository)
    {
        _userManager = userManager;
        _configuration = configuration;
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

            var result = await _userManager.CreateAsync(user, clientRequest.Password);
            if (!result.Succeeded) throw new Exception(result.Errors.First().Description);

            await _userManager.AddToRoleAsync(user, Role.Client.ToString());

            var userId = await _userManager.GetUserIdAsync(user);

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

            _clientRepository.Ajouter(client, userId);

            return new BaseResponse(HttpStatusCode.OK, "Compte créé avec succès");
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            return new BaseResponse(HttpStatusCode.Conflict, "Un compte avec cet email existe déjà");
        }
        catch (Exception e)
        {
            return new BaseResponse(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}