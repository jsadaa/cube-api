using ApiCube.Persistence.Models;

namespace ApiCube.Persistence.Repositories.Client;

public interface IClientRepository
{
    Task Ajouter(Domain.Entities.Client nouveauClient, ApplicationUserModel applicationUserModel, string password);

    public List<Domain.Entities.Client> Lister();
    public Domain.Entities.Client Trouver(int id);
    public Task Modifier(Domain.Entities.Client client, string password);
    public Task Supprimer(Domain.Entities.Client client);
}