using System.Net;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Factories;
using ApiCube.Persistence.Repositories.FamilleProduit;
using AutoMapper;

namespace ApiCube.Application.Services.FamilleProduit;

public class FamilleProduitService : IFamilleProduitService
{
    private readonly IFamilleProduitRepository _familleProduitRepository;
    private readonly IMapper _mapper;
    
    public FamilleProduitService(IFamilleProduitRepository familleProduitRepository, IMapper mapper)
    {
        _familleProduitRepository = familleProduitRepository;
        _mapper = mapper;
    }
    
    public BaseResponse AjouterUneFamilleProduit(FamilleProduitRequestDTO familleProduitRequestDTO)
    {
        try
        {
            _familleProduitRepository.Ajouter(familleProduitRequestDTO);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Famille de produit ajoutée avec succès" }
            );
            
            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );
            
            return response;
        }
    }
    
    public BaseResponse ListerLesFamillesProduits()
    {
        try
        {
            var famillesProduits = _familleProduitRepository.Lister();
            var famillesProduitsResponse = _mapper.Map<List<FamilleProduitResponseDTO>>(famillesProduits);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { famillesProduitsResponse }
            );
            
            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );
            
            return response;
        }
    }
}