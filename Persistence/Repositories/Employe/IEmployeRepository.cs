using ApiCube.Persistence.Models;

namespace ApiCube.Persistence.Repositories.Employe;

public interface IEmployeRepository
{
    public Task Ajouter(Domain.Entities.Employe employe, ApplicationUserModel applicationUserModel, string password);
}