using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Application.Services.Produit;

public interface IProduitService
{
    public BaseResponse AjouterUnProduitAuCatalogue(AjouterProduitRequest produitRequest);
    
    public BaseResponse ListerLesProduits();
}