using ApiCube.Domain.Factories;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.TransactionStock;

public class TransactionStockRepository : ITransactionStockRepository
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;
    private readonly TransactionStockFactory _transactionStockFactory;
    
    public TransactionStockRepository(ApiDbContext context, IMapper mapper, TransactionStockFactory transactionStockFactory)
    {
        _context = context;
        _mapper = mapper;
        _transactionStockFactory = transactionStockFactory;
    }
    
    public int Ajouter(Domain.Entities.TransactionStock transactionStock)
    {
        var nouvelleTransactionStock = _mapper.Map<TransactionStockModel>(transactionStock);
        
        _context.TransactionsStock.Add(nouvelleTransactionStock);
        _context.SaveChanges();
        
        return nouvelleTransactionStock.Id;
    }

    public Domain.Entities.TransactionStock Trouver(int id)
    {
        var transactionStock = _context.TransactionsStock
            .Include(transactionStock => transactionStock.Produit)
            .FirstOrDefault(transactionStock => transactionStock.Id == id);
        
        if (transactionStock == null) throw new TransactionStockIntrouvable();
        
        return _mapper.Map<Domain.Entities.TransactionStock>(transactionStock);
    }
    
    public Domain.Entities.TransactionStock Trouver(string nom)
    {
        var transactionStock = _context.TransactionsStock
            .Include(transactionStock => transactionStock.Produit)
            .FirstOrDefault(transactionStock => transactionStock.Produit.Nom == nom);

        if (transactionStock == null) throw new TransactionStockIntrouvable();
        
        return _mapper.Map<Domain.Entities.TransactionStock>(transactionStock);
    }
    
    public List<Domain.Entities.TransactionStock> Lister()
    {
        var transactionsStock = _context.TransactionsStock
            .Include(transactionStock => transactionStock.Produit)
            .ToList();
        
        return transactionsStock.Select(transactionStock => _transactionStockFactory.Mapper(transactionStock)).ToList();
    }
    
    public List<Domain.Entities.TransactionStock> ListerParStock(int idStock)
    {
        var transactionsStock = _context.TransactionsStock
            .Include(transactionStock => transactionStock.Produit)
            .Where(transactionStock => transactionStock.StockId == idStock)
            .ToList();
        
        return transactionsStock.Select(transactionStock => _transactionStockFactory.Mapper(transactionStock)).ToList();
    }

    public void Modifier(Domain.Entities.TransactionStock transactionStockModifiee)
    {
        var transactionStockModel = _mapper.Map<TransactionStockModel>(transactionStockModifiee);
        
        _context.TransactionsStock.Update(transactionStockModel);
        _context.SaveChanges();
    }

    public void Supprimer(Domain.Entities.TransactionStock transactionStock)
    {
        var transactionStockModel = _mapper.Map<TransactionStockModel>(transactionStock);
        
        _context.TransactionsStock.Remove(transactionStockModel);
        _context.SaveChanges();
    }
}