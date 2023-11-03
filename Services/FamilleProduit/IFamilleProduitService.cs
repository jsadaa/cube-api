using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Services.FamilleProduit;

public interface IFamilleProduitService
{
    public BaseResponse AjouterUneFamilleProduit(AjouterFamilleProduitRequest familleProduitRequest);
    
    public BaseResponse ListerLesFamillesProduits();
}