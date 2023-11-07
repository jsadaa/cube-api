using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Repositories.Promotion;
using AutoMapper;

namespace ApiCube.Application.Services.Promotion;

public class PromotionService : IPromotionService
{
    
    private readonly IPromotionRepository _promotionRepository;
    private readonly IMapper _mapper;
    
    public PromotionService(
        IPromotionRepository promotionRepository,
        IMapper mapper
    )
    {
        _promotionRepository = promotionRepository;
        _mapper = mapper;
    }
    
    public BaseResponse AjouterUnePromotion(PromotionRequestDTO promotionRequestDTO)
    {
        try
        {
            var nouvellePromotion = new Domain.Entities.Promotion(
                nom: promotionRequestDTO.Nom,
                description: promotionRequestDTO.Description,
                dateDebut: promotionRequestDTO.DateDebut,
                dateFin: promotionRequestDTO.DateFin,
                pourcentage: promotionRequestDTO.Pourcentage
            );
            _promotionRepository.Ajouter(nouvellePromotion);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Promotion ajoutée avec succès" }
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
    
    public BaseResponse ListerLesPromotions()
    {
        try
        {
            var listePromotions = _promotionRepository.Lister();
            var promotions = _mapper.Map<List<PromotionResponseDTO>>(listePromotions);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: promotions
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