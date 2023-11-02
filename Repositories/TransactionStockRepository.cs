using ApiCube.Domain.Entities;
using ApiCube.DTOs.Responses;
using ApiCube.Models;
using ApiCube.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Repositories;

public class TransactionStockRepository : ITransactionStockRepository
{
    private readonly ApiDbContext _context;
    
    public TransactionStockRepository(ApiDbContext context)
    {
        _context = context;
    }
    
    public void Ajouter(TransactionStock transactionStock)
    {
        TransactionStockModel nouvelleTransactionStock = new TransactionStockModel
        {
            Quantite = transactionStock.Quantite,
            Date = transactionStock.Date,
            ProduitId = transactionStock.Produit.Id
        };

        using (_context)
        {
            _context.TransactionsStock.Add(nouvelleTransactionStock);
            _context.SaveChanges();
        }
    }

    public TransactionStockDTO? Trouver(int id)
    {
        TransactionStockDTO? transactionStock = null;
        
        using (_context)
        {
            transactionStock = _context.TransactionsStock
                .Include(transactionStock => transactionStock.Produit)
                .Select(transactionStock => new TransactionStockDTO
                {
                    Id = transactionStock.Id,
                    Quantite = transactionStock.Quantite,
                    Date = transactionStock.Date,
                    Type = transactionStock.Type,
                    ProduitNom = transactionStock.Produit.Nom,
                    PrixUnitaire = transactionStock.PrixUnitaire,
                    PrixTotal = transactionStock.PrixTotal
                })
                .FirstOrDefault(transactionStock => transactionStock.Id == id);
        }
        
        return transactionStock;
    }
    
    public List<TransactionStockDTO> Lister()
    {
        List<TransactionStockDTO> transactionsStock = new List<TransactionStockDTO>();

        using (_context)
        {
            transactionsStock.AddRange(
                _context.TransactionsStock
                    .Include(transactionStock => transactionStock.Produit)
                    .Select(transactionStock => new TransactionStockDTO
                    {
                        Id = transactionStock.Id,
                        Quantite = transactionStock.Quantite,
                        Date = transactionStock.Date,
                        Type = transactionStock.Type,
                        ProduitNom = transactionStock.Produit.Nom,
                        PrixUnitaire = transactionStock.PrixUnitaire,
                        PrixTotal = transactionStock.PrixTotal
                    })
            );
        }

        return transactionsStock;
    }
    
    public void Modifier(int id, TransactionStock transactionStock)
    {
        TransactionStockModel? transactionStockAModifier = null;

        using (_context)
        {
            transactionStockAModifier = _context.TransactionsStock.Find(id);
        }
        
        if (transactionStockAModifier == null) return;
        
        transactionStockAModifier.Quantite = transactionStock.Quantite;
        transactionStockAModifier.Date = transactionStock.Date;
        transactionStockAModifier.Type = transactionStock.Type.ToString();
        transactionStockAModifier.ProduitId = transactionStock.Produit.Id;
        
        using (_context)
        {
            _context.TransactionsStock.Update(transactionStockAModifier);
            _context.SaveChanges();
        }
    }

    public void Supprimer(int id)
    {
        TransactionStockModel? transactionStockASupprimer = null;

        using (_context)
        {
            transactionStockASupprimer = _context.TransactionsStock.Find(id);
        }

        if (transactionStockASupprimer == null) return;

        using (_context)
        {
            _context.TransactionsStock.Remove(transactionStockASupprimer);
            _context.SaveChanges();
        }
    }
}