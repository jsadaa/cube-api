using ApiCube.Domain.Enums.Stock;
using ApiCube.Domain.Mappers.FamilleProduit;
using ApiCube.Domain.Mappers.Fournisseur;
using ApiCube.Domain.Mappers.Produit;
using ApiCube.Domain.Mappers.Stock;
using ApiCube.Domain.Mappers.TransactionStock;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.TransactionStock;

public class TransactionStockRepository : ITransactionStockRepository
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITransactionStockMapper _transactionStockMapper;
    private readonly IFamilleProduitMapper _familleProduitMapper;
    private readonly IFournisseurMapper _fournisseurMapper;
    private readonly IStockMapper _stockMapper;
    private readonly IProduitMapper _produitMapper;
    private readonly TypeTransactionStockMapper _typeTransactionStockMapper;

    public TransactionStockRepository(ApiDbContext context, IMapper mapper,
        ITransactionStockMapper transactionStockMapper, IFamilleProduitMapper familleProduitMapper,
        IFournisseurMapper fournisseurMapper, IStockMapper stockMapper, IProduitMapper produitMapper,
        TypeTransactionStockMapper typeTransactionStockMapper)
    {
        _context = context;
        _mapper = mapper;
        _transactionStockMapper = transactionStockMapper;
        _familleProduitMapper = familleProduitMapper;
        _fournisseurMapper = fournisseurMapper;
        _stockMapper = stockMapper;
        _produitMapper = produitMapper;
        _typeTransactionStockMapper = typeTransactionStockMapper;
    }

    public void Ajouter(Domain.Entities.TransactionStock transactionStock)
    {
        var nouvelleTransactionStock = _mapper.Map<TransactionStockModel>(transactionStock);

        _context.TransactionsStock.Add(nouvelleTransactionStock);
        _context.SaveChanges();
    }

    public Domain.Entities.TransactionStock Trouver(int id)
    {
        var transactionStock = _context.TransactionsStock
            .Include(transactionStock => transactionStock.Stock)
            .Include(transactionStock => transactionStock.Stock.Produit)
            .Include(transactionStock => transactionStock.Stock.Produit.FamilleProduit)
            .Include(transactionStock => transactionStock.Stock.Produit.Fournisseur)
            .FirstOrDefault(transactionStock => transactionStock.Id == id);

        if (transactionStock == null) throw new TransactionStockIntrouvable();

        var familleProduit = _familleProduitMapper.Mapper(transactionStock.Stock.Produit.FamilleProduit);
        var fournisseur = _fournisseurMapper.Mapper(transactionStock.Stock.Produit.Fournisseur);
        var produit = _produitMapper.Mapper(transactionStock.Stock.Produit, familleProduit, fournisseur);
        var stock = _stockMapper.MapperSansTransactionsStock(transactionStock.Stock, produit);
        var typeTransactionStock = _typeTransactionStockMapper.Mapper(transactionStock.Type);

        return _transactionStockMapper.Mapper(transactionStock, stock, typeTransactionStock);
    }

    public List<Domain.Entities.TransactionStock> Lister()
    {
        var transactionsStockModels = _context.TransactionsStock
            .Include(transactionStock => transactionStock.Stock)
            .Include(transactionStock => transactionStock.Stock.Produit)
            .Include(transactionStock => transactionStock.Stock.Produit.FamilleProduit)
            .Include(transactionStock => transactionStock.Stock.Produit.Fournisseur)
            .ToList();

        List<Domain.Entities.TransactionStock> transactionsStockMapped = new();

        foreach (var transactionStock in transactionsStockModels)
        {
            var familleProduit = _familleProduitMapper.Mapper(transactionStock.Stock.Produit.FamilleProduit);
            var fournisseur = _fournisseurMapper.Mapper(transactionStock.Stock.Produit.Fournisseur);
            var produit = _produitMapper.Mapper(transactionStock.Stock.Produit, familleProduit, fournisseur);
            var stock = _stockMapper.MapperSansTransactionsStock(transactionStock.Stock, produit);
            var typeTransactionStock = _typeTransactionStockMapper.Mapper(transactionStock.Type);

            transactionsStockMapped.Add(_transactionStockMapper.Mapper(transactionStock, stock, typeTransactionStock));
        }

        return transactionsStockMapped;
    }

    public List<Domain.Entities.TransactionStock> ListerParStock(int idStock)
    {
        var transactionsStock = _context.TransactionsStock
            .Include(transactionStock => transactionStock.Stock)
            .Include(transactionStock => transactionStock.Stock.Produit)
            .Include(transactionStock => transactionStock.Stock.Produit.FamilleProduit)
            .Include(transactionStock => transactionStock.Stock.Produit.Fournisseur)
            .Where(transactionStock => transactionStock.Stock.Id == idStock)
            .ToList();

        List<Domain.Entities.TransactionStock> transactionsStockMapped = new();

        foreach (var transactionStock in transactionsStock)
        {
            var familleProduit = _familleProduitMapper.Mapper(transactionStock.Stock.Produit.FamilleProduit);
            var fournisseur = _fournisseurMapper.Mapper(transactionStock.Stock.Produit.Fournisseur);
            var produit = _produitMapper.Mapper(transactionStock.Stock.Produit, familleProduit, fournisseur);
            var stock = _stockMapper.MapperSansTransactionsStock(transactionStock.Stock, produit);
            var typeTransactionStock = _typeTransactionStockMapper.Mapper(transactionStock.Type);

            transactionsStockMapped.Add(_transactionStockMapper.Mapper(transactionStock, stock, typeTransactionStock));
        }

        return transactionsStockMapped;
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