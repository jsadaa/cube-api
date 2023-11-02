using ApiCube.Domain.Entities;
using ApiCube.DTOs;
using ApiCube.DTOs.Responses;

namespace ApiCube.Repositories.Interfaces;

public interface IProduitRepository
{
    public void AjouterProduit(Produit produit);
    
    public List<ProduitDTO> ListerProduits();
    
    public ProduitDTO? TrouverProduit(int id);
    
    public void ModifierProduit(int id, Produit produit);
    
    public void SupprimerProduit(int id);
}