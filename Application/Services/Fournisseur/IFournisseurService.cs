using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.Fournisseur;

public interface IFournisseurService
{
    public BaseResponse AjouterUnFournisseur(FournisseurRequest fournisseurRequest);

    public BaseResponse ListerLesFournisseurs();

    public BaseResponse TrouverUnFournisseur(int id);

    public BaseResponse ModifierUnFournisseur(int id, FournisseurRequest fournisseurRequest);

    public BaseResponse SupprimerUnFournisseur(int id);
}