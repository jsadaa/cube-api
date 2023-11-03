using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Services.Fournisseur;

public interface IFournisseurService
{
    public BaseResponse AjouterUnFournisseur(AjouterFournisseurRequest fournisseurRequest);
    
    public BaseResponse ListerLesFournisseurs();
}