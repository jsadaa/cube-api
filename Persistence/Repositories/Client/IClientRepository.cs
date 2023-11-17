namespace ApiCube.Persistence.Repositories.Client;

public interface IClientRepository
{
    public void Ajouter(Domain.Entities.Client nouveauClient, string idApplicationUser);
    /*public List<Domain.Entities.Client> Lister();
    public Domain.Entities.Client Trouver(int id);
    public Domain.Entities.Client Trouver(string username);
    public void MettreAJour(Domain.Entities.Client client);
    public void Supprimer(Domain.Entities.Client client);*/
}