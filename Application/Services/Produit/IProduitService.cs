using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Requests.Produit;

namespace ApiCube.Application.Services.Produit;

public interface IProduitService
{
    public BaseResponse AjouterUnProduitAuCatalogue(ProduitRequest produitRequest);

    public BaseResponse ListerLesProduits();

    public BaseResponse AppliquerUnePromotion(int produitId, int promotionId);

    public BaseResponse RetirerUnePromotion(int produitId);

    public BaseResponse TrouverUnProduit(int produitId);

    public BaseResponse ModifierUnProduit(int produitId, ProduitUpdate produitUpdate);

    public BaseResponse SupprimerUnProduit(int produitId);
}