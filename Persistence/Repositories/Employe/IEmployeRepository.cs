namespace ApiCube.Persistence.Repositories.Employe;

public interface IEmployeRepository
{
    public void Ajouter(Domain.Entities.Employe employe);
    public List<Domain.Entities.Employe> Lister();
    public Domain.Entities.Employe Trouver(int id);
    public void Modifier(Domain.Entities.Employe employe);
}