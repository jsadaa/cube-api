using System.Globalization;
using System.Net;
using System.Text;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Exceptions;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Administration;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using ApiCube.Persistence.Repositories.Client;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.Client;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUserModel> _userManager;

    public ClientService(IClientRepository clientRepository, IMapper mapper,
        UserManager<ApplicationUserModel> userManager)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<BaseResponse> AjouterUnClient(ClientRequest clientRequest)
    {
        try
        {
            // Normalisation du nom d'utilisateur
            var userName = clientRequest.Nom + clientRequest.Prenom;
            userName = userName.Normalize(NormalizationForm.FormD);
            var chars = userName.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
            userName = new string(chars.ToArray());
            userName = userName.Replace(" ", "");

            var applicationUserModel = new ApplicationUserModel
            {
                UserName = userName,
                Email = clientRequest.Email,
                EmailConfirmed = true
            };

            var creationAppUser = await _userManager.CreateAsync(applicationUserModel, clientRequest.Password);
            if (!creationAppUser.Succeeded)
            {
                var firstError = creationAppUser.Errors.First();
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
                        throw new Exception(firstError.Code);
                }
            }

            await _userManager.AddToRoleAsync(applicationUserModel, Role.Client.ToString());
            var appUserId = await _userManager.GetUserIdAsync(applicationUserModel);

            var client = new Domain.Entities.Client(
                clientRequest.Nom + clientRequest.Prenom,
                clientRequest.Nom,
                clientRequest.Prenom,
                clientRequest.Adresse,
                clientRequest.CodePostal,
                clientRequest.Ville,
                clientRequest.Pays,
                clientRequest.Telephone,
                clientRequest.Email,
                clientRequest.DateNaissance,
                DateTime.Now,
                appUserId
            );

            var id = _clientRepository.Ajouter(client);
            
            var clientResponse = _mapper.Map<ClientResponse>(client);
            clientResponse.Id = id;

            var response = new BaseResponse(
                HttpStatusCode.Created,
                clientResponse
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
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse ListerLesClients()
    {
        try
        {
            var listeClients = _clientRepository.Lister();
            var clients = _mapper.Map<List<ClientResponse>>(listeClients);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                clients
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse TrouverUnClient(int id)
    {
        try
        {
            var client = _clientRepository.Trouver(id);
            var clientResponse = _mapper.Map<ClientResponse>(client);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                clientResponse
            );

            return response;
        }
        catch (ClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    /*public async Task<BaseResponse> ModifierUnClient(int id, ClientRequest clientRequest)
    {
        try
        {
            var client = _clientRepository.Trouver(id);
            var applicationUser = await _userManager.FindByEmailAsync(client.Email);

            if (applicationUser == null) throw new UtilisateurIntrouvable();

            // Normalisation du nom d'utilisateur
            var userName = clientRequest.Nom + clientRequest.Prenom;
            userName = userName.Normalize(NormalizationForm.FormD);
            var chars = userName.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
            userName = new string(chars.ToArray());
            userName = userName.Replace(" ", "");

            applicationUser.Email = clientRequest.Email;
            applicationUser.UserName = userName;

            var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
            var resetPassword = await _userManager.ResetPasswordAsync(applicationUser, token, clientRequest.Password);

            if (!resetPassword.Succeeded)
            {
                var firstError = resetPassword.Errors.First();
                switch (firstError.Code)
                {
                    case "PasswordTooShort":
                    case "PasswordRequiresDigit":
                    case "PasswordRequiresLower":
                    case "PasswordRequiresUpper":
                    case "PasswordRequiresUniqueChars":
                    case "PasswordRequiresNonAlphanumeric":
                        throw new FormatMotDePasseInvalide();
                    default:
                        throw new Exception("error_reset_password");
                }
            }

            var updateAppUser = await _userManager.UpdateAsync(applicationUser);
            if (!updateAppUser.Succeeded)
            {
                var firstError = updateAppUser.Errors.First();
                switch (firstError.Code)
                {
                    case "DuplicateUserName":
                    case "DuplicateEmail":
                        throw new UtilisateurExisteDeja();
                    default:
                        throw new Exception("error_update_user");
                }
            }

            var appUserId = await _userManager.GetUserIdAsync(applicationUser);

            client.MettreAJour(
                clientRequest.Nom,
                clientRequest.Prenom,
                clientRequest.Adresse,
                clientRequest.CodePostal,
                clientRequest.Ville,
                clientRequest.Pays,
                clientRequest.Telephone,
                clientRequest.Email,
                clientRequest.DateNaissance,
                appUserId
            );

            _clientRepository.Modifier(client);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "client_modifie" }
            );

            return response;
        }
        catch (ClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (UtilisateurIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
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
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = "client_existe_deja" }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }*/

    public async Task<BaseResponse> ModifierUnClient(int id, ClientUpdate clientUpdate)
    {
        try
        {
            var client = _clientRepository.Trouver(id);
            var applicationUser = await _userManager.FindByEmailAsync(client.Email);

            if (applicationUser == null) throw new UtilisateurIntrouvable();

            // Normalisation du nom d'utilisateur
            var userName = clientUpdate.Nom + clientUpdate.Prenom;
            userName = userName.Normalize(NormalizationForm.FormD);
            var chars = userName.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
            userName = new string(chars.ToArray());
            userName = userName.Replace(" ", "");

            applicationUser.Email = clientUpdate.Email;
            applicationUser.UserName = userName;

            var updateAppUser = await _userManager.UpdateAsync(applicationUser);
            if (!updateAppUser.Succeeded)
            {
                var firstError = updateAppUser.Errors.First();
                switch (firstError.Code)
                {
                    case "DuplicateUserName":
                    case "DuplicateEmail":
                        throw new UtilisateurExisteDeja();
                    default:
                        throw new Exception("error_update_user");
                }
            }

            var appUserId = await _userManager.GetUserIdAsync(applicationUser);

            client.MettreAJour(
                clientUpdate.Nom,
                clientUpdate.Prenom,
                clientUpdate.Adresse,
                clientUpdate.CodePostal,
                clientUpdate.Ville,
                clientUpdate.Pays,
                clientUpdate.Telephone,
                clientUpdate.Email,
                clientUpdate.DateNaissance,
                appUserId
            );

            _clientRepository.Modifier(client);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "client_modifie" }
            );

            return response;
        }
        catch (ClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (UtilisateurIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
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
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = "client_existe_deja" }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public async Task<BaseResponse> SupprimerUnClient(int id)
    {
        try
        {
            var client = _clientRepository.Trouver(id);
            var applicationUser = await _userManager.FindByEmailAsync(client.Email);

            if (applicationUser == null) throw new UtilisateurIntrouvable();

            // Ici on supprime l'utilisateur et le client
            // Pas besoin de supprimer le client avec le repository car il est supprimé en cascade avec l'utilisateur
            var deleteAppUser = await _userManager.DeleteAsync(applicationUser);

            if (!deleteAppUser.Succeeded) throw new Exception("error_delete_user");

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "client_supprime" }
            );

            return response;
        }
        catch (ClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (UtilisateurIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }
}