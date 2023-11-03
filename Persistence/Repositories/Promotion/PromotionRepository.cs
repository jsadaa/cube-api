using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Models;

namespace ApiCube.Persistence.Repositories.Promotion;

public class PromotionRepository : IPromotionRepository
{
    private readonly ApiDbContext _context;
    
    public PromotionRepository(ApiDbContext context)
    {
        _context = context;
    }
    
    public int Ajouter(AjouterPromotionRequest promotion)
    {
        PromotionModel nouvellePromotion = new PromotionModel
        {
            Nom = promotion.Nom,
            Description = promotion.Description,
            DateDebut = promotion.DateDebut,
            DateFin = promotion.DateFin,
            Pourcentage = promotion.Pourcentage,
        };
        
        _context.Promotions.Add(nouvellePromotion);
        _context.SaveChanges();
        
        return nouvellePromotion.Id;
    }
    
    public PromotionDTO? Trouver(int id)
    {
        PromotionModel? promotion = null;
        promotion = _context.Promotions.Find(id);
            
        if (promotion == null)
        {
            return null;
        }
        
        return new PromotionDTO
        {
            Id = promotion.Id,
            Nom = promotion.Nom,
            Description = promotion.Description,
            DateDebut = promotion.DateDebut,
            DateFin = promotion.DateFin,
            Pourcentage = promotion.Pourcentage
        };
    }
    
    public List<PromotionDTO> Lister()
    {
        List<PromotionDTO> promotions = new List<PromotionDTO>();
        
        promotions.AddRange(
            _context.Promotions
                .Select(promotion => new PromotionDTO()
                {
                    Id = promotion.Id,
                    Nom = promotion.Nom,
                    Description = promotion.Description,
                    DateDebut = promotion.DateDebut,
                    DateFin = promotion.DateFin,
                    Pourcentage = promotion.Pourcentage
                })
        );
        
        return promotions;
    }
    
    public int? Modifier(int id, AjouterPromotionRequest promotion)
    {
        PromotionModel? promotionAModifier = null;
        promotionAModifier = _context.Promotions.Find(id);
        
        if (promotionAModifier == null)
        {
            return null;
        }
        
        promotionAModifier.Nom = promotion.Nom;
        promotionAModifier.Description = promotion.Description;
        promotionAModifier.DateDebut = promotion.DateDebut;
        promotionAModifier.DateFin = promotion.DateFin;
        promotionAModifier.Pourcentage = promotion.Pourcentage;
        
        _context.Promotions.Update(promotionAModifier);
        _context.SaveChanges();
        
        return promotionAModifier.Id;
    }
    
    public void Supprimer(int id)
    {
        PromotionModel? promotion = null;
        promotion = _context.Promotions.Find(id);
        
        if (promotion == null)
        {
            return;
        }
        
        _context.Promotions.Remove(promotion);
        _context.SaveChanges();
    }
}