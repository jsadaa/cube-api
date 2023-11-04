using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Application.Services.Fournisseur;

public interface IFournisseurService
{
    public BaseResponse AjouterUnFournisseur(AjouterFournisseurRequest fournisseurRequest);
    
    public BaseResponse ListerLesFournisseurs();
}