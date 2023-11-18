namespace ApiCube.Persistence.Repositories.Client;

public interface IClientRepository
{
    public void Ajouter(Domain.Entities.Client nouveauClient, string password);
    public List<Domain.Entities.Client> Lister();
    public Domain.Entities.Client Trouver(int id);
    public void Modifier(Domain.Entities.Client client, string applicationUserId);
}