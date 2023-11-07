using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Promotion;

public interface IPromotionMapper
{
    public Entities.Promotion Mapper(PromotionModel promotionModel);
}