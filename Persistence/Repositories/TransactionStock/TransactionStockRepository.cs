using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.TransactionStock;

public class TransactionStockRepository : ITransactionStockRepository
{
    private readonly ApiDbContext _context;
    
    public TransactionStockRepository(ApiDbContext context)
    {
        _context = context;
    }
    
    public int Ajouter(TransactionStockDTO transactionStock)
    {
        TransactionStockModel nouvelleTransactionStock = new TransactionStockModel
        {
            Quantite = transactionStock.Quantite,
            Date = transactionStock.Date,
            Type = transactionStock.Type,
            ProduitId = transactionStock.ProduitId,
            PrixUnitaire = transactionStock.PrixUnitaire,
            PrixTotal = transactionStock.PrixTotal
        };
        
        _context.TransactionsStock.Add(nouvelleTransactionStock);
        _context.SaveChanges();
        
        return nouvelleTransactionStock.Id;
    }

    public TransactionStockDTO? Trouver(int id)
    {
        TransactionStockDTO? transactionStock = null;
        
        transactionStock = _context.TransactionsStock
            .Include(transactionStock => transactionStock.Produit)
            .Select(transactionStock => new TransactionStockDTO
            {
                Id = transactionStock.Id,
                Quantite = transactionStock.Quantite,
                Date = transactionStock.Date,
                Type = transactionStock.Type,
                ProduitId = transactionStock.ProduitId,
                PrixUnitaire = transactionStock.PrixUnitaire,
                PrixTotal = transactionStock.PrixTotal
            })
            .FirstOrDefault(transactionStock => transactionStock.Id == id);
        
        return transactionStock;
    }
    
    public List<TransactionStockDTO> Lister()
    {
        List<TransactionStockDTO> transactionsStock = new List<TransactionStockDTO>();

        transactionsStock.AddRange(
            _context.TransactionsStock
                .Include(transactionStock => transactionStock.Produit)
                .Select(transactionStock => new TransactionStockDTO
                {
                    Id = transactionStock.Id,
                    Quantite = transactionStock.Quantite,
                    Date = transactionStock.Date,
                    Type = transactionStock.Type,
                    ProduitId = transactionStock.ProduitId,
                    PrixUnitaire = transactionStock.PrixUnitaire,
                    PrixTotal = transactionStock.PrixTotal
                })
        );
        
        return transactionsStock;
    }
    
    public List<TransactionStockDTO> ListerParStock(int idStock)
    {
        List<TransactionStockDTO> transactionsStock = new List<TransactionStockDTO>();

        // TODO:
        // search where transactionStock.Produit.StockId == idStock
        
        return transactionsStock;
    }
    
    public int? Modifier(int id, TransactionStockDTO transactionStock)
    {
        TransactionStockModel? transactionStockAModifier = null;
        transactionStockAModifier = _context.TransactionsStock.Find(id);
        
        if (transactionStockAModifier == null) return null;
        
        transactionStockAModifier.Quantite = transactionStock.Quantite;
        transactionStockAModifier.Date = transactionStock.Date;
        transactionStockAModifier.Type = transactionStock.Type;
        transactionStockAModifier.ProduitId = transactionStock.ProduitId;
        
        _context.TransactionsStock.Update(transactionStockAModifier);
        _context.SaveChanges();
        
        return transactionStockAModifier.Id;
    }

    public void Supprimer(int id)
    {
        TransactionStockModel? transactionStockASupprimer = null;
        transactionStockASupprimer = _context.TransactionsStock.Find(id);

        if (transactionStockASupprimer == null) return;
        
        _context.TransactionsStock.Remove(transactionStockASupprimer);
        _context.SaveChanges();
    }
}