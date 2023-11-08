using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.FamilleProduit;

public interface IFamilleProduitService
{
    public BaseResponse AjouterUneFamilleProduit(FamilleProduitRequest familleProduitRequest);

    public BaseResponse ListerLesFamillesProduits();
    
    public BaseResponse TrouverUneFamilleProduit(int id);
    
    public BaseResponse ModifierUneFamilleProduit(int id, FamilleProduitRequest familleProduitRequest);
    
    public BaseResponse SupprimerUneFamilleProduit(int id);
}