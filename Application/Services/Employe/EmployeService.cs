using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.Exceptions;
using ApiCube.Domain.Enums.Administration;
using ApiCube.Persistence.Models;
using ApiCube.Persistence.Repositories.Employe;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.Employe;

public class EmployeService : IEmployeService
{
    private readonly UserManager<ApplicationUserModel> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IEmployeRepository _employeRepository;

    public EmployeService(UserManager<ApplicationUserModel> userManager, IConfiguration configuration,
        IEmployeRepository employeRepository)
    {
        _userManager = userManager;
        _configuration = configuration;
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

            var result = await _userManager.CreateAsync(user, employeRequest.Password);
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

            await _userManager.AddToRoleAsync(user, Role.Employe.ToString());

            var userId = await _userManager.GetUserIdAsync(user);

            var employe = new Domain.Entities.Employe(
                nom: employeRequest.Nom,
                prenom: employeRequest.Prenom,
                email: employeRequest.Email,
                dateEmbauche: employeRequest.DateEmbauche,
                statut: employeRequest.Statut
            );

            _employeRepository.Ajouter(employe, userId);

            var response = new BaseResponse(
                HttpStatusCode.Created,
                "L'employé a été ajouté avec succès"
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