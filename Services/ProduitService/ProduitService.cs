using System.Net;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Factories;
using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;
using ApiCube.Repositories.Interfaces;

namespace ApiCube.Services.ProduitService;

public class ProduitService : IProduitService
{
    private readonly IProduitRepository _produitRepository;
    private readonly ProduitFactory _produitFactory;
    
    public ProduitService(IProduitRepository produitRepository, ProduitFactory produitFactory)
    {
        _produitRepository = produitRepository;
        _produitFactory = produitFactory;
    }

    public BaseResponse AjouterUnProduitAuStock(AjouterProduitRequest produitRequest)
    {
        try
        {
            Produit nouveauProduit = _produitFactory.CreerProduit(produitRequest);
            _produitRepository.Ajouter(nouveauProduit);
            
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Produit ajouté au stock avec succès" }
            );
            
            return response;
        }
        catch (Exception e)
        {
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
                );
            
            return response;
        }
    }
    
    public BaseResponse ListerLesProduits()
    {
        try
        {
            List<ProduitDTO> produits = _produitRepository.Lister();
            
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: produits
            );
            
            return response;
        }
        catch (Exception e)
        {
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );
            
            return response;
        }
    }
}