using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.Promotion;

public interface IPromotionService
{
    public BaseResponse AjouterUnePromotion(PromotionRequest promotionRequest);
    public BaseResponse ListerLesPromotions();
    public BaseResponse TrouverUnePromotion(int id);
    public BaseResponse ModifierUnePromotion(int id, PromotionRequest promotionRequest);
    public BaseResponse SupprimerUnePromotion(int id);
}