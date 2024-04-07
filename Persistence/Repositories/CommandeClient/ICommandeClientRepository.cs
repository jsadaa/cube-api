namespace ApiCube.Persistence.Repositories.CommandeClient;

public interface ICommandeClientRepository
{
    public void Ajouter(Domain.Entities.CommandeClient commandeClient);
    public Domain.Entities.CommandeClient Trouver(int id);
    public Domain.Entities.CommandeClient TrouverParUuid(Guid uuid);
    public List<Domain.Entities.CommandeClient> Lister();
    public List<Domain.Entities.CommandeClient> ListerParClient(int idClient);
    public void Modifier(Domain.Entities.CommandeClient commandeClient);
    public void Supprimer(Domain.Entities.CommandeClient commandeClient);
}