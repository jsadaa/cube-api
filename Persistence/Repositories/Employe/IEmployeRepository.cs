namespace ApiCube.Persistence.Repositories.Employe;

public interface IEmployeRepository
{
    public void Ajouter(Domain.Entities.Employe employe, string userId);
}