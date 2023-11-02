using System.Net;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Factories;
using ApiCube.DTOs;
using ApiCube.DTOs.Responses;
using ApiCube.Repositories;
using ApiCube.Repositories.Interfaces;

namespace ApiCube.Services.ProduitService;

public class ProduitService : IProduitService
{
    public IProduitRepository ProduitRepository;
    public readonly ProduitFactory ProduitFactory;
    
    public ProduitService(IProduitRepository produitRepository, ProduitFactory produitFactory)
    {
        ProduitRepository = produitRepository;
        ProduitFactory = produitFactory;
    }

    public BaseResponse AjouterUnProduitAuStock(AjouterProduitRequest produitRequest)
    {
        try
        {
            Produit nouveauProduit = ProduitFactory.CreerProduit(produitRequest);
            ProduitRepository.AjouterProduit(nouveauProduit);
            
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
            List<ProduitDTO> produits = ProduitRepository.ListerProduits();
            
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