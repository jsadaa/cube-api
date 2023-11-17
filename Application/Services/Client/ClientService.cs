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
            if (!result.Succeeded)
            {
                var firstError = result.Errors.First();
                switch (firstError.Code)
                {
                    case "DuplicateUserName":
                    case "DuplicateEmail":
                        throw new UtilisateurExisteDeja();
                    case "PasswordTooShort":
                    case "PasswordRequiresDigit":
                    case "PasswordRequiresLower":
                    case "PasswordRequiresUpper":
                    case "PasswordRequiresUniqueChars":
                    case "PasswordRequiresNonAlphanumeric":
                        throw new FormatMotDePasseInvalide();
                    default:
                        throw new Exception("Erreur lors de la création de l'utilisateur");
                }
            }

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

            var response = new BaseResponse(
                HttpStatusCode.Created,
                "Le client a été ajouté avec succès"
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                "L'employé existe déjà"
            );

            return response;
        }
        catch (UtilisateurExisteDeja)
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                "Un utilisateur avec ce nom ou cet email existe déjà"
            );

            return response;
        }
        catch (FormatMotDePasseInvalide)
        {
            var response = new BaseResponse(
                HttpStatusCode.BadRequest,
                "Le mot de passe doit contenir au moins 6 caractères dont une majuscule, une minuscule, un chiffre et un caractère spécial"
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                e.Message
            );

            return response;
        }
    }
}