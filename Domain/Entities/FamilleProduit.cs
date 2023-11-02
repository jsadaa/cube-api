namespace ApiCube.Domain.Entities;

public class FamilleProduit
{
    
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Description { get; set; }
    private ICollection<Produit?> Produits { get; set; } = new List<Produit?>();
    
    public FamilleProduit(string nom, string description)
    {
        Nom = nom;
        Description = description;
    }
    
    public void AjouterProduitALaFamille(Produit produit)
    {
        Produits.Add(produit);
    }
    
    public void RetirerProduitDeLaFamille(Produit produit)
    {
        Produits.Remove(produit);
    }
    
    public ICollection<Produit?> ObtenirProduits()
    {
        return Produits;
    }
}