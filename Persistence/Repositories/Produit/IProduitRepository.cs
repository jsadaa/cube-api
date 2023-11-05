namespace ApiCube.Persistence.Repositories.Produit;

public interface IProduitRepository
{
    public int Ajouter(Domain.Entities.Produit nouveauProduit);
    
    public List<Domain.Entities.Produit> Lister();
    
    public Domain.Entities.Produit Trouver(int id);
    
    public Domain.Entities.Produit Trouver(string nom);
    
    public void Modifier(Domain.Entities.Produit produitModifie);
    
    public void Supprimer(Domain.Entities.Produit produitSupprime);
}