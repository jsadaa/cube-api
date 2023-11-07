using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.Produit;

public interface IProduitService
{
    public BaseResponse AjouterUnProduitAuCatalogue(ProduitRequestDTO produitRequestDTO);

    public BaseResponse ListerLesProduits();

    public BaseResponse AppliquerUnePromotion(int produitId, int promotionId);

    public BaseResponse RetirerUnePromotion(int produitId);
}