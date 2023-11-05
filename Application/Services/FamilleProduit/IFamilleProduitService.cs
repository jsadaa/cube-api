using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Application.Services.FamilleProduit;

public interface IFamilleProduitService
{
    public BaseResponse AjouterUneFamilleProduit(FamilleProduitRequestDTO familleProduitRequestDTO);
    
    public BaseResponse ListerLesFamillesProduits();
}