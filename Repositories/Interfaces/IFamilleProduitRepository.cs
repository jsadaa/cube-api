using ApiCube.Domain.Entities;
using ApiCube.DTOs.Responses;

namespace ApiCube.Repositories.Interfaces;

public interface IFamilleProduitRepository
{
    
    public void Ajouter(FamilleProduit familleProduit);
    
    public List<FamilleProduitDTO> Lister();
    
    public FamilleProduitDTO? Trouver(int id);
    
    public void Modifier(int id, FamilleProduit familleProduit);
    
    public void Supprimer(int id);
}