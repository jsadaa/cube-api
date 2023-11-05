namespace ApiCube.Domain.Entities;

public class FamilleProduit
{
    
    public int Id { get; set; } = 0;
    public string Nom { get; set; }
    public string Description { get; set; }
    private ICollection<Produit?> Produits { get; set; } = new List<Produit?>();
    
    public FamilleProduit(string nom, string description)
    {
        Nom = nom;
        Description = description;
    }
    
    public FamilleProduit(int id, string nom, string description)
    {
        Id = id;
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
    
    public void MettreAJour(string nom, string description)
    {
        Nom = nom;
        Description = description;
    }
    
    public ICollection<Produit?> ObtenirProduits()
    {
        return Produits;
    }
}