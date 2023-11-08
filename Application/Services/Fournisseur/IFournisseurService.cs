using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.Fournisseur;

public interface IFournisseurService
{
    public BaseResponse AjouterUnFournisseur(FournisseurRequestDTO fournisseurRequestDTO);

    public BaseResponse ListerLesFournisseurs();
    
    public BaseResponse TrouverUnFournisseurParId(int id);
    
    public BaseResponse ModifierUnFournisseur(int id, FournisseurRequestDTO fournisseurRequestDTO);
    
    public BaseResponse SupprimerUnFournisseur(int id);
}