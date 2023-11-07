using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Application.Services.Produit;

public interface IProduitService
{
    public BaseResponse AjouterUnProduitAuCatalogue(ProduitRequestDTO produitRequestDTO);

    public BaseResponse ListerLesProduits();
    
    public BaseResponse AppliquerUnePromotion(int produitId, int promotionId);
    
    public BaseResponse RetirerUnePromotion(int produitId);
}