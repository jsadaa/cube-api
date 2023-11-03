using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Persistence.Repositories.Produit;

public interface IProduitRepository
{
    public int Ajouter(AjouterProduitRequest produit);
    
    public List<ProduitDTO> Lister();
    
    public ProduitDTO? Trouver(int id);
    
    public int? Modifier(int id, AjouterProduitRequest produit);
    
    public void Supprimer(int id);
}