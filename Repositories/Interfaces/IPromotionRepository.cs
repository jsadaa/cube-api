using ApiCube.Domain.Entities;
using ApiCube.DTOs;

namespace ApiCube.Repositories.Interfaces;

public interface IPromotionRepository
{
    public void AjouterPromotion(Promotion promotion);
    public AjouterPromotionRequest? TrouverPromotion(int id);
    public List<AjouterPromotionRequest> ListerPromotions();
    public void ModifierPromotion(int id, Promotion promotion);
    public void SupprimerPromotion(int id);
}