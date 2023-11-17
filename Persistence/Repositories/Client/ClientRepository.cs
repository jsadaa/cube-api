using ApiCube.Application.Exceptions;
using ApiCube.Domain.Enums.Administration;
using ApiCube.Domain.Mappers.Client;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

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
        var clients = _context.Clients.ToList();
        return clients.Select(client => _clientMapper.Mapper(client)).ToList();
    }
}