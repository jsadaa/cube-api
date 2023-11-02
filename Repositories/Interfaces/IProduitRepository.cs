using ApiCube.Domain.Entities;
using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Repositories.Interfaces;

public interface IProduitRepository
{
    public void Ajouter(AjouterProduitRequest produit);
    
    public List<ProduitDTO> Lister();
    
    public ProduitDTO? Trouver(int id);
    
    public void Modifier(int id, AjouterProduitRequest produit);
    
    public void Supprimer(int id);
}