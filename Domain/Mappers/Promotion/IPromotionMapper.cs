using ApiCube.Application.DTOs.Requests;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Promotion;

public interface IPromotionMapper
{
    public Domain.Entities.Promotion Mapper(PromotionModel promotionModel);
}