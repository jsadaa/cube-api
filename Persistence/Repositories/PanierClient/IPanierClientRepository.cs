namespace ApiCube.Persistence.Repositories.PanierClient;

public interface IPanierClientRepository
{
    public void Ajouter(Domain.Entities.PanierClient nouveauPanierClient);
    public List<Domain.Entities.PanierClient> Lister();
    public Domain.Entities.PanierClient Trouver(int id);
    public void Modifier(Domain.Entities.PanierClient panierClient);
    public void Supprimer(Domain.Entities.PanierClient panierClient);
}