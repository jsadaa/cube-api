namespace ApiCube.Persistence.Repositories.Fournisseur;

public interface IFournisseurRepository
{
    public void Ajouter(Domain.Entities.Fournisseur nouveauFournisseur);
    public List<Domain.Entities.Fournisseur> Lister();
    public Domain.Entities.Fournisseur Trouver(int id);
    public Domain.Entities.Fournisseur Trouver(string nom);
    public Domain.Entities.Fournisseur TrouverParProduit(int id);
    public void Modifier(Domain.Entities.Fournisseur fournisseurModifie);
    public void Supprimer(Domain.Entities.Fournisseur fournisseurSupprime);
}