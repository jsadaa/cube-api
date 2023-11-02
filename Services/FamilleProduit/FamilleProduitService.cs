using System.Net;
using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;
using ApiCube.Repositories.Interfaces;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Factories;

namespace ApiCube.Services.FamilleProduit;

public class FamilleProduitService : IFamilleProduitService
{
    private readonly IFamilleProduitRepository _familleProduitRepository;
    private readonly FamilleProduitFactory _familleProduitFactory;
    
    public FamilleProduitService(IFamilleProduitRepository familleProduitRepository, FamilleProduitFactory familleProduitFactory)
    {
        _familleProduitRepository = familleProduitRepository;
        _familleProduitFactory = familleProduitFactory;
    }
    
    public BaseResponse AjouterFamilleProduit(AjouterFamilleProduitRequest familleProduitRequest)
    {
        try
        {
            Domain.Entities.FamilleProduit nouvelleFamilleProduit = _familleProduitFactory.CreerFamilleProduit(familleProduitRequest);
            _familleProduitRepository.Ajouter(familleProduitRequest);
            
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Famille de produit ajoutée avec succès" }
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
    
    public BaseResponse ListerFamillesProduits()
    {
        try
        {
            List<FamilleProduitDTO> famillesProduits = _familleProduitRepository.Lister();
            
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { famillesProduits }
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