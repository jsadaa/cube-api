namespace ApiCube.Persistence.Repositories.Stock;

public interface IStockRepository
{
    public void Ajouter(Domain.Entities.Stock nouveauStock);
    public List<Domain.Entities.Stock> Lister();
    public Domain.Entities.Stock Trouver(int id);
    public Domain.Entities.Stock Trouver(string nom);
    public void Modifier(Domain.Entities.Stock stock);
    public void Supprimer(Domain.Entities.Stock stock);
}