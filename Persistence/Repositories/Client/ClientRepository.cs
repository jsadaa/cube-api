using ApiCube.Application.Exceptions;
using ApiCube.Domain.Enums.Administration;
using ApiCube.Domain.Mappers.Client;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Client;

public class ClientRepository : IClientRepository
{
    private readonly ApiDbContext _context;
    private readonly IClientMapper _clientMapper;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUserModel> _userManager;

    public ClientRepository(ApiDbContext context, IClientMapper clientMapper, IMapper mapper,
        UserManager<ApplicationUserModel> userManager)
    {
        _context = context;
        _clientMapper = clientMapper;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task Ajouter(Domain.Entities.Client nouveauClient, ApplicationUserModel applicationUserModel,
        string password)
    {
        var creationAppUser = await _userManager.CreateAsync(applicationUserModel, password);
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
                    throw new Exception("Erreur lors de la création de l'utilisateur");
            }
        }

        await _userManager.AddToRoleAsync(applicationUserModel, Role.Client.ToString());

        var userId = await _userManager.GetUserIdAsync(applicationUserModel);
        var nouveauClientModel = _mapper.Map<ClientModel>(nouveauClient);
        nouveauClientModel.ApplicationUserId = userId;

        _context.Clients.Add(nouveauClientModel);
        await _context.SaveChangesAsync();
    }

    public List<Domain.Entities.Client> Lister()
    {
        var clients = _context.Clients.AsNoTracking().ToList();
        return clients.Select(client => _clientMapper.Mapper(client)).ToList();
    }

    public Domain.Entities.Client Trouver(int id)
    {
        var client = _context.Clients.AsNoTracking().FirstOrDefault(client => client.Id == id);
        if (client == null)
        {
            throw new ClientIntrouvable();
        }

        return _clientMapper.Mapper(client);
    }

    public async Task Modifier(Domain.Entities.Client client, string password)
    {
        var clientModifié = _mapper.Map<ClientModel>(client);
        var applicationUser = await _userManager.FindByEmailAsync(clientModifié.Email);

        if (applicationUser == null)
        {
            throw new UtilisateurIntrouvable();
        }

        applicationUser.Email = clientModifié.Email;
        applicationUser.UserName = clientModifié.Nom + clientModifié.Prenom;

        var token = _userManager.GeneratePasswordResetTokenAsync(applicationUser).Result;
        var resetPassword = _userManager.ResetPasswordAsync(applicationUser, token, password).Result;

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
                    throw new Exception("Erreur lors de la modification de l'utilisateur");
            }
        }

        var updateAppUser = _userManager.UpdateAsync(applicationUser).Result;

        if (!updateAppUser.Succeeded)
        {
            var firstError = updateAppUser.Errors.First();
            switch (firstError.Code)
            {
                case "DuplicateUserName":
                case "DuplicateEmail":
                    throw new UtilisateurExisteDeja();
                default:
                    throw new Exception("Erreur lors de la modification de l'utilisateur");
            }
        }

        clientModifié.ApplicationUser = applicationUser;
        _context.Clients.Update(clientModifié);
        await _context.SaveChangesAsync();
    }

    public async Task Supprimer(Domain.Entities.Client client)
    {
        var clientSupprimé = _mapper.Map<ClientModel>(client);
        var applicationUser = await _userManager.FindByEmailAsync(clientSupprimé.Email);

        if (applicationUser == null)
        {
            throw new UtilisateurIntrouvable();
        }

        // Ici on supprime l'utilisateur et le client
        // Pas besoin de supprimer le client avec le contexte car il est supprimé en cascade avec l'utilisateur
        var deleteAppUser = await _userManager.DeleteAsync(applicationUser);

        if (!deleteAppUser.Succeeded)
        {
            throw new Exception("error_delete_user");
        }
    }
}