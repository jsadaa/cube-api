using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Promotion;

public class PromotionMapper : IPromotionMapper
{
    public Entities.Promotion Mapper(PromotionModel promotionModel)
    {
        return new Entities.Promotion( 
            promotionModel.Id,
            promotionModel.Nom,
            promotionModel.Description,
            promotionModel.Pourcentage,
            promotionModel.DateDebut,
            promotionModel.DateFin
        );
    }
}