using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Exceptions;
using ApiCube.Domain.Enums.Administration;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using ApiCube.Persistence.Repositories.Employe;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.Employe;

public class EmployeService : IEmployeService
{
    private readonly IEmployeRepository _employeRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUserModel> _userManager;

    public EmployeService(IEmployeRepository employeRepository, IMapper mapper,
        UserManager<ApplicationUserModel> userManager)
    {
        _employeRepository = employeRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<BaseResponse> AjouterUnEmploye(EmployeRequest employeRequest)
    {
        try
        {
            var applicationUserModel = new ApplicationUserModel
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
                poste: employeRequest.Poste
            );

            var creationAppUser = await _userManager.CreateAsync(applicationUserModel, employeRequest.Password);
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
                        throw new Exception("error_create_user");
                }
            }

            await _userManager.AddToRoleAsync(applicationUserModel, Role.Employe.ToString());
            var userId = await _userManager.GetUserIdAsync(applicationUserModel);

            _employeRepository.Ajouter(employe, userId);

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

    public BaseResponse ListerLesEmployes()
    {
        var listeEmployes = _employeRepository.Lister();
        var employes = _mapper.Map<List<EmployeResponse>>(listeEmployes);

        var response = new BaseResponse(
            HttpStatusCode.OK,
            employes
        );

        return response;
    }

    public BaseResponse TrouverUnEmploye(int id)
    {
        var employe = _employeRepository.Trouver(id);
        var employeResponse = _mapper.Map<EmployeResponse>(employe);

        var response = new BaseResponse(
            HttpStatusCode.OK,
            employeResponse
        );

        return response;
    }

    public async Task<BaseResponse> ModifierUnEmploye(int id, EmployeRequest employeRequest)
    {
        try
        {
            var employe = _employeRepository.Trouver(id);
            var applicationUser = await _userManager.FindByEmailAsync(employe.Email);

            if (applicationUser == null)
            {
                throw new UtilisateurIntrouvable();
            }

            employe.MettreAJour(
                nom: employeRequest.Nom,
                prenom: employeRequest.Prenom,
                email: employeRequest.Email,
                dateEmbauche: employeRequest.DateEmbauche,
                poste: employeRequest.Poste
            );

            applicationUser.Email = employeRequest.Email;
            applicationUser.UserName = employeRequest.Nom + employeRequest.Prenom;

            var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
            var resetPassword = await _userManager.ResetPasswordAsync(applicationUser, token, employeRequest.Password);

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

            var updateUtilisateur = await _userManager.UpdateAsync(applicationUser);
            if (!updateUtilisateur.Succeeded)
            {
                var firstError = updateUtilisateur.Errors.First();
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
            _employeRepository.Modifier(employe, appUserId);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "employe_modifie" }
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

    public async Task<BaseResponse> SupprimerUnEmploye(int id)
    {
        try
        {
            var employe = _employeRepository.Trouver(id);
            var applicationUser = await _userManager.FindByEmailAsync(employe.Email);

            if (applicationUser == null)
            {
                throw new UtilisateurIntrouvable();
            }

            // Ici on supprime l'utilisateur et l'employé
            // Pas besoin de supprimer l'employé avec le repository car il est supprimé en cascade avec l'utilisateur
            var deleteAppUser = await _userManager.DeleteAsync(applicationUser);
            if (!deleteAppUser.Succeeded)
            {
                throw new Exception("error_delete_user");
            }

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "employe_supprime" }
            );

            return response;
        }
        catch (EmployeIntrouvable e)
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