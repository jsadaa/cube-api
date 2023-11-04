using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Persistence.Repositories.FamilleProduit;

public interface IFamilleProduitRepository
{
    
    public int Ajouter(FamilleProduitRequestDTO familleProduit);
    
    public List<Domain.Entities.FamilleProduit> Lister();
    
    public Domain.Entities.FamilleProduit? Trouver(int id);
    public Domain.Entities.FamilleProduit Trouver(string nom);
    
    public int? Modifier(int id, FamilleProduitRequestDTO familleProduitRequest);
    
    public void Supprimer(int id);
}