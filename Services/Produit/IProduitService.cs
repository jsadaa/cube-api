using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Services.Produit;

public interface IProduitService
{
    public BaseResponse AjouterUnProduitAuStock(AjouterProduitRequest produitRequest);
    
    public BaseResponse ListerLesProduits();
}