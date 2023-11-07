namespace ApiCube.Persistence.Repositories.FamilleProduit;

public interface IFamilleProduitRepository
{
    public void Ajouter(Domain.Entities.FamilleProduit nouvelleFamilleProduit);

    public List<Domain.Entities.FamilleProduit> Lister();

    public Domain.Entities.FamilleProduit Trouver(int id);
    public Domain.Entities.FamilleProduit Trouver(string nom);

    public void Modifier(Domain.Entities.FamilleProduit familleProduitModifiee);

    public void Supprimer(Domain.Entities.FamilleProduit familleProduitSupprimee);
}