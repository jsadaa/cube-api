using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.CommandeClient;

public interface ICommandeClientService
{
    public BaseResponse CreerUnPanier(int idClient);
    public BaseResponse AjouterUnProduitAuPanier(int id, ProduitPanierRequest produitPanierRequest);
    public BaseResponse TrouverUnPanier(int id);
    public BaseResponse ListerLesPaniersDUnClient(int idClient);
}