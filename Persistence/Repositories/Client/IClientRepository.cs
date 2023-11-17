using ApiCube.Persistence.Models;

namespace ApiCube.Persistence.Repositories.Client;

public interface IClientRepository
{
    Task Ajouter(Domain.Entities.Client nouveauClient, ApplicationUserModel applicationUserModel, string password);
    public List<Domain.Entities.Client> Lister();
    /*public Domain.Entities.Client Trouver(int id);
    public Domain.Entities.Client Trouver(string username);
    public void MettreAJour(Domain.Entities.Client client);
    public void Supprimer(Domain.Entities.Client client);*/
}