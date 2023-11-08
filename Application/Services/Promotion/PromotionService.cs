using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Exceptions;
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

    public BaseResponse AjouterUnePromotion(PromotionRequest promotionRequest)
    {
        try
        {
            var nouvellePromotion = new Domain.Entities.Promotion(
                nom: promotionRequest.Nom,
                description: promotionRequest.Description,
                dateDebut: promotionRequest.DateDebut,
                dateFin: promotionRequest.DateFin,
                pourcentage: promotionRequest.Pourcentage
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
            var promotions = _mapper.Map<List<PromotionResponse>>(listePromotions);

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
    
    public BaseResponse TrouverUnePromotion(int id)
    {
        try
        {
            var promotion = _promotionRepository.Trouver(id);
            var promotionResponse = _mapper.Map<PromotionResponse>(promotion);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: promotionResponse
            );

            return response;
        }
        catch (PromotionIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = e.Message }
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
    
    public BaseResponse ModifierUnePromotion(int id, PromotionRequest promotionRequest)
    {
        try
        {
            var promotionAModifier = _promotionRepository.Trouver(id);
            
            promotionAModifier.MettreAJour(
                nom: promotionRequest.Nom,
                description: promotionRequest.Description,
                dateDebut: promotionRequest.DateDebut,
                dateFin: promotionRequest.DateFin,
                pourcentage: promotionRequest.Pourcentage
            );
            _promotionRepository.Modifier(promotionAModifier);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { message = "Promotion modifiée avec succès" }
            );

            return response;
        }
        catch (PromotionIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = e.Message }
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

    public BaseResponse SupprimerUnePromotion(int id)
    {
        try
        {
            var promotionASupprimer = _promotionRepository.Trouver(id);
            _promotionRepository.Supprimer(promotionASupprimer);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { message = "Promotion supprimée avec succès" }
            );

            return response;
        }
        catch (PromotionIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = e.Message }
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