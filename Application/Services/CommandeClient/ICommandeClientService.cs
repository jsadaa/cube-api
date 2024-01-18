using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.CommandeClient;

public interface ICommandeClientService
{
    public BaseResponse CreerUnPanier(int idClient);
    public BaseResponse AjouterUnProduitAuPanier(int id, ProduitPanierRequest produitPanierRequest);
    public BaseResponse TrouverUnPanier(int id);
    public BaseResponse ListerLesPaniersDUnClient(int idClient);
    public BaseResponse ModifierLaQuantiteDUnProduitDansLePanier(int id, ProduitPanierUpdate produitPanierUpdate);
    public BaseResponse ViderUnPanier(int id);
    public BaseResponse SupprimerUnProduitDuPanier(int id, int idProduit);
    public BaseResponse SupprimerUnPanier(int id);
    public BaseResponse ValiderUnPanier(int id);
    public BaseResponse TrouverUneCommande(int id);
    public BaseResponse ListerLesCommandesDUnClient(int idClient);
    public BaseResponse ListerLesCommandes();
}