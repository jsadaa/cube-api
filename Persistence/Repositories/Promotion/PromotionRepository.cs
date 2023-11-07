using ApiCube.Domain.Mappers.Promotion;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Promotion;

public class PromotionRepository : IPromotionRepository
{
    private readonly ApiDbContext _context;
    private readonly IPromotionMapper _promotionMapper;
    private readonly IMapper _mapper;

    public PromotionRepository(ApiDbContext context, IPromotionMapper promotionMapper, IMapper mapper)
    {
        _context = context;
        _promotionMapper = promotionMapper;
        _mapper = mapper;
    }

    public void Ajouter(Domain.Entities.Promotion nouvellePromotion)
    {
        var nouvellePromotionModel = _mapper.Map<PromotionModel>(nouvellePromotion);

        _context.Promotions.Add(nouvellePromotionModel);
        _context.SaveChanges();
    }

    public List<Domain.Entities.Promotion> Lister()
    {
        var promotionsModels = _context.Promotions.AsNoTracking().ToList();

        return promotionsModels.Select(promotionModel => _promotionMapper.Mapper(promotionModel)).ToList();
    }

    public Domain.Entities.Promotion Trouver(int id)
    {
        var promotionModel = _context.Promotions.AsNoTracking().FirstOrDefault(promotion => promotion.Id == id);

        if (promotionModel == null) throw new PromotionIntrouvable();

        return _promotionMapper.Mapper(promotionModel);
    }

    public void Modifier(Domain.Entities.Promotion promotion)
    {
        var promotionModel = _mapper.Map<PromotionModel>(promotion);

        _context.Promotions.Update(promotionModel);
        _context.SaveChanges();
    }

    public void Supprimer(Domain.Entities.Promotion promotion)
    {
        var promotionModel = _mapper.Map<PromotionModel>(promotion);

        _context.Promotions.Remove(promotionModel);
        _context.SaveChanges();
    }
}