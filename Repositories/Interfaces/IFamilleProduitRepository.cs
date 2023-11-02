using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Repositories.Interfaces;

public interface IFamilleProduitRepository
{
    
    public void Ajouter(AjouterFamilleProduitRequest familleProduit);
    
    public List<FamilleProduitDTO> Lister();
    
    public FamilleProduitDTO? Trouver(int id);
    
    public void Modifier(int id, AjouterFamilleProduitRequest familleProduit);
    
    public void Supprimer(int id);
}