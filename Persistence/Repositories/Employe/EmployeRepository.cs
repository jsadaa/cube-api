using ApiCube.Application.Exceptions;
using ApiCube.Domain.Enums.Administration;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ApiCube.Persistence.Repositories.Employe;

public class EmployeRepository : IEmployeRepository
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUserModel> _userManager;

    public EmployeRepository(ApiDbContext context, IMapper mapper, UserManager<ApplicationUserModel> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task Ajouter(Domain.Entities.Employe employe,
        string password)
    {
        var employeModel = _mapper.Map<Domain.Entities.Employe, EmployeModel>(employe);
        var applicationUserModel = new ApplicationUserModel
        {
            UserName = employe.Nom + employe.Prenom,
            Email = employe.Email,
            EmailConfirmed = true
        };

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

        await _userManager.AddToRoleAsync(applicationUserModel, Role.Employe.ToString());

        var userId = await _userManager.GetUserIdAsync(applicationUserModel);
        employeModel.ApplicationUserId = userId;

        _context.Employes.Add(employeModel);
        await _context.SaveChangesAsync();
    }
}