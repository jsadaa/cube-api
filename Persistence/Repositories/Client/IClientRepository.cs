namespace ApiCube.Persistence.Repositories.Client;

public interface IClientRepository
{
    public int Ajouter(Domain.Entities.Client nouveauClient);
    public List<Domain.Entities.Client> Lister();
    public Domain.Entities.Client Trouver(int id);
    public Domain.Entities.Client TrouverParApplicationUserId(string applicationUserId);
    public void Modifier(Domain.Entities.Client client);
}