using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Factories;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Stock;

public class StockRepository : IStockRepository
{
    private readonly ApiDbContext _context;
    private readonly StockFactory _stockFactory;
    private readonly IMapper _mapper;
    
    public StockRepository(ApiDbContext context, StockFactory stockFactory, IMapper mapper)
    {
        _context = context;
        _stockFactory = stockFactory;
        _mapper = mapper;
    }
    
    public int Ajouter(Domain.Entities.Stock nouveauStock)
    {
        var nouveauStockModel = _mapper.Map<StockModel>(nouveauStock);
        
        _context.Stocks.Add(nouveauStockModel);
        _context.SaveChanges();
        
        return nouveauStockModel.Id;
    }
    
    public List<Domain.Entities.Stock> Lister()
    {
        var stocksModels = _context.Stocks
            .Include(stock => stock.Produit).ThenInclude(produitModel => produitModel.FamilleProduit)
            .Include(stockModel => stockModel.Produit).ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(stockModel => stockModel.TransactionsStock)
            .ToList();
        
        return stocksModels.Select(stockModel => _stockFactory.Mapper(stockModel)).ToList();
    }

    public Domain.Entities.Stock Trouver(int id)
    {
        var stockModel = _context.Stocks
            .Include(stock => stock.Produit).ThenInclude(produitModel => produitModel.FamilleProduit)
            .Include(stockModel => stockModel.Produit).ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(stockModel => stockModel.TransactionsStock)
            .FirstOrDefault(stock => stock.Id == id);
        
        if (stockModel == null) throw new StockIntrouvable();
        
        return _stockFactory.Mapper(stockModel);
    }

    public Domain.Entities.Stock Trouver(string nom)
    {
        var stockModel = _context.Stocks
            .Include(stock => stock.Produit).ThenInclude(produitModel => produitModel.FamilleProduit)
            .Include(stockModel => stockModel.Produit).ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(stockModel => stockModel.TransactionsStock)
            .FirstOrDefault(stock => stock.Produit.Nom == nom);
        
        if (stockModel == null) throw new StockIntrouvable();
        
        return _stockFactory.Mapper(stockModel);
    }
    
    public void Modifier(Domain.Entities.Stock stockModifie)
    {
        var stockModifieModel = _mapper.Map<StockModel>(stockModifie);
        
        _context.Stocks.Update(stockModifieModel);
        _context.SaveChanges();
    }
    
    public void Supprimer(Domain.Entities.Stock stockASupprimer)
    {
        var stockASupprimerModel = _mapper.Map<StockModel>(stockASupprimer);
        
        _context.Stocks.Remove(stockASupprimerModel);
        _context.SaveChanges();
    }
}