namespace ApiCube.Persistence.Repositories.FactureClient;

public interface IFactureClientRepository
{
    public void Ajouter(Domain.Entities.FactureClient factureClient);
    public Domain.Entities.FactureClient Trouver(int id);
    public List<Domain.Entities.FactureClient> Lister();
    public void Modifier(Domain.Entities.FactureClient factureClient);
    public void Supprimer(Domain.Entities.FactureClient factureClient);
}