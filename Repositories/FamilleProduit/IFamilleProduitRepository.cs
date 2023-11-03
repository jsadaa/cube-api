using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Repositories.FamilleProduit;

public interface IFamilleProduitRepository
{
    
    public int Ajouter(AjouterFamilleProduitRequest familleProduit);
    
    public List<FamilleProduitDTO> Lister();
    
    public FamilleProduitDTO? Trouver(int id);
    
    public int? Modifier(int id, AjouterFamilleProduitRequest familleProduit);
    
    public void Supprimer(int id);
}