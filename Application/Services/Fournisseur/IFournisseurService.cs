using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.Fournisseur;

public interface IFournisseurService
{
    public BaseResponse AjouterUnFournisseur(FournisseurRequestDTO fournisseurRequestDTO);

    public BaseResponse ListerLesFournisseurs();
}