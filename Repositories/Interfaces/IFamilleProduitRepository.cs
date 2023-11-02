using ApiCube.Domain.Entities;
using ApiCube.DTOs;

namespace ApiCube.Repositories.Interfaces;

public interface IFamilleProduitRepository
{
    
    public void AjouterFamilleProduit(FamilleProduit familleProduit);
    
    public List<FamilleProduitDTO> ListerFamillesProduits();
    
    public FamilleProduitDTO TrouverFamilleProduit(int id);
    
    public void ModifierFamilleProduit(int id, FamilleProduit familleProduit);
    
    public void SupprimerFamilleProduit(int id);
}