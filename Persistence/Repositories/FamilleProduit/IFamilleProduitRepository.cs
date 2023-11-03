using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Persistence.Repositories.FamilleProduit;

public interface IFamilleProduitRepository
{
    
    public int Ajouter(AjouterFamilleProduitRequest familleProduit);
    
    public List<FamilleProduitDTO> Lister();
    
    public FamilleProduitDTO? Trouver(int id);
    public FamilleProduitDTO? Trouver(string nom);
    
    public int? Modifier(int id, AjouterFamilleProduitRequest familleProduit);
    
    public void Supprimer(int id);
}