using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Services.FamilleProduit;

public interface IFamilleProduitService
{
    public BaseResponse AjouterFamilleProduit(AjouterFamilleProduitRequest familleProduitRequest);
    
    public BaseResponse ListerFamillesProduits();
}