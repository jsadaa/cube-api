using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Services.ProduitService;

public interface IProduitService
{
    public BaseResponse AjouterUnProduitAuStock(AjouterProduitRequest produitRequest);
    
    public BaseResponse ListerLesProduits();
}