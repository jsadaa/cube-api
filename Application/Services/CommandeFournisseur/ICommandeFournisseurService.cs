using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.CommandeFournisseur;

public interface ICommandeFournisseurService
{
    public BaseResponse AjouterUneCommandeFournisseur(CommandeFournisseurRequest commandeFournisseurRequest);
    public BaseResponse ModifierUneCommandeFournisseur(int id, CommandeFournisseurRequest commandeFournisseurRequest);
    public BaseResponse ListerLesCommandesFournisseurs();
    public BaseResponse TrouverUneCommandeFournisseur(int id);
    public BaseResponse SupprimerUneCommandeFournisseur(int id);
    public BaseResponse MarquerUneCommandeFournisseurCommeLivree(int id);
}