using ApiCube.Domain.Entities;
using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;
using ApiCube.Models;
using ApiCube.Repositories.Interfaces;

namespace ApiCube.Repositories;

public class PromotionRepository : IPromotionRepository
{
    private readonly ApiDbContext _context;
    
    public PromotionRepository(ApiDbContext context)
    {
        _context = context;
    }
    
    public void Ajouter(AjouterPromotionRequest promotion)
    {
        PromotionModel nouvellePromotion = new PromotionModel
        {
            Nom = promotion.Nom,
            Description = promotion.Description,
            DateDebut = promotion.DateDebut,
            DateFin = promotion.DateFin,
            Pourcentage = promotion.Pourcentage,
            ProduitId = promotion.ProduitId
        };
        
        _context.Promotions.Add(nouvellePromotion);
        _context.SaveChanges();
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
    
    public void Modifier(int id, AjouterPromotionRequest promotion)
    {
        PromotionModel? promotionModifiée = null;
        promotionModifiée = _context.Promotions.Find(id);
        
        if (promotionModifiée == null)
        {
            return;
        }
        
        promotionModifiée.Nom = promotion.Nom;
        promotionModifiée.Description = promotion.Description;
        promotionModifiée.DateDebut = promotion.DateDebut;
        promotionModifiée.DateFin = promotion.DateFin;
        promotionModifiée.Pourcentage = promotion.Pourcentage;
        promotionModifiée.ProduitId = promotion.ProduitId;
        
        _context.Promotions.Update(promotionModifiée);
        _context.SaveChanges();
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