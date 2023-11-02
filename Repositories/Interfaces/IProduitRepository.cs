using ApiCube.Domain.Entities;
using ApiCube.DTOs.Responses;

namespace ApiCube.Repositories.Interfaces;

public interface IProduitRepository
{
    public void Ajouter(Produit produit);
    
    public List<ProduitDTO> Lister();
    
    public ProduitDTO? Trouver(int id);
    
    public void Modifier(int id, Produit produit);
    
    public void Supprimer(int id);
}