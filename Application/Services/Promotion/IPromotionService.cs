using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.Promotion;

public interface IPromotionService
{
    public BaseResponse AjouterUnePromotion(PromotionRequestDTO promotionRequestDTO);
    public BaseResponse ListerLesPromotions();
}