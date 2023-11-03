using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Stock;

public class StockRepository
{
    private readonly ApiDbContext _context;
    
    public StockRepository(ApiDbContext context)
    {
        _context = context;
    }
    
    public int Ajouter(AjouterStockRequest stock)
    {
        StockModel nouveauStock = new StockModel
        {
            ProduitId = stock.ProduitId,
            Quantite = stock.Quantite,
            SeuilDisponibilite = stock.SeuilDisponibilite,
            Statut = stock.Statut,
            DateCreation = DateTime.Now,
            DateModification = DateTime.Now
        };
        
        _context.Stocks.Add(nouveauStock);
        _context.SaveChanges();
        
        return nouveauStock.Id;
    }
    
    public List<StockDTO?> Lister()
    {
        List<StockDTO?> stocks = new List<StockDTO?>();
        
        stocks.AddRange(
            _context.Stocks
                .Include(stock => stock.Produit)
                .Select(stock => new StockDTO
                {
                    Id = stock.Id,
                    Quantite = stock.Quantite,
                    SeuilDisponibilite = stock.SeuilDisponibilite,
                    Statut = stock.Statut,
                    Produit = new ProduitDTO
                    {
                        Id = stock.Produit.Id,
                        Nom = stock.Produit.Nom,
                        Description = stock.Produit.Description,
                        Appellation = stock.Produit.Appellation,
                        Cepage = stock.Produit.Cepage,
                        Region = stock.Produit.Region,
                        DegreAlcool = stock.Produit.DegreAlcool,
                        FamilleProduitNom = stock.Produit.FamilleProduit.Nom,
                        FournisseurNom = stock.Produit.Fournisseur.Nom,
                        PrixAchat = stock.Produit.PrixAchat,
                        PrixVente = stock.Produit.PrixVente,
                        EnPromotion = stock.Produit.EnPromotion
                    },
                    DateCreation = stock.DateCreation,
                    DatePeremption = stock.DatePeremption,
                    DateModification = stock.DateModification,
                    DateSuppression = stock.DateSuppression
                })
        );
        
        return stocks;
    }
    
    public StockDTO? Trouver(int id)
    {
        StockModel? stock = null;
        stock = _context.Stocks
            .Include(stock => stock.Produit).ThenInclude(produitModel => produitModel.FamilleProduit)
            .Include(stockModel => stockModel.Produit).ThenInclude(produitModel => produitModel.Fournisseur)
            .FirstOrDefault(stock => stock.Id == id);
        
        if (stock == null)
        {
            return null;
        }
        
        return new StockDTO
        {
            Id = stock.Id,
            Quantite = stock.Quantite,
            SeuilDisponibilite = stock.SeuilDisponibilite,
            Statut = stock.Statut,
            Produit = new ProduitDTO
            {
                Id = stock.Produit.Id,
                Nom = stock.Produit.Nom,
                Description = stock.Produit.Description,
                Appellation = stock.Produit.Appellation,
                Cepage = stock.Produit.Cepage,
                Region = stock.Produit.Region,
                DegreAlcool = stock.Produit.DegreAlcool,
                FamilleProduitNom = stock.Produit.FamilleProduit.Nom,
                FournisseurNom = stock.Produit.Fournisseur.Nom,
                PrixAchat = stock.Produit.PrixAchat,
                PrixVente = stock.Produit.PrixVente,
                EnPromotion = stock.Produit.EnPromotion
            },
            DateCreation = stock.DateCreation,
            DatePeremption = stock.DatePeremption,
            DateModification = stock.DateModification,
            DateSuppression = stock.DateSuppression
        };
    }
    
    public int? Modifier(int id, AjouterStockRequest stock)
    {
        StockModel? stockAModifier = null;
        stockAModifier = _context.Stocks.Find(id);

        if (stockAModifier == null) return null;
        
        stockAModifier.Quantite = stock.Quantite;
        stockAModifier.SeuilDisponibilite = stock.SeuilDisponibilite;
        stockAModifier.Statut = stock.Statut;
        stockAModifier.DateModification = DateTime.Now;
        
        _context.Stocks.Update(stockAModifier);
        _context.SaveChanges();
        
        return stockAModifier.Id;
    }
    
    public void Supprimer(int id)
    {
        StockModel? stock = null;
        stock = _context.Stocks.Find(id);
        
        if (stock == null) return;
        
        _context.Stocks.Remove(stock);
        _context.SaveChanges();
    }
}