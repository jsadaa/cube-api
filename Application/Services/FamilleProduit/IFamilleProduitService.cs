using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Application.Services.FamilleProduit;

public interface IFamilleProduitService
{
    public BaseResponse AjouterUneFamilleProduit(AjouterFamilleProduitRequest familleProduitRequest);
    
    public BaseResponse ListerLesFamillesProduits();
}