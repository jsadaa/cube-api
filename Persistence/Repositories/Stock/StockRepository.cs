using ApiCube.Domain.Enums.Stock;
using ApiCube.Domain.Mappers.FamilleProduit;
using ApiCube.Domain.Mappers.Fournisseur;
using ApiCube.Domain.Mappers.Produit;
using ApiCube.Domain.Mappers.Stock;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Stock;

public class StockRepository : IStockRepository
{
    private readonly ApiDbContext _context;
    private readonly StatutStockMapper _statutStockMapper;
    private readonly IStockMapper _stockMapper;
    private readonly IProduitMapper _produitMapper;
    private readonly IFamilleProduitMapper _familleProduitMapper;
    private readonly IFournisseurMapper _fournisseurMapper;
    private readonly IMapper _mapper;

    public StockRepository(ApiDbContext context, StatutStockMapper statutStockMapper, IStockMapper stockMapper,
        IProduitMapper produitMapper, IFamilleProduitMapper familleProduitMapper, IFournisseurMapper fournisseurMapper,
        IMapper mapper)
    {
        _context = context;
        _statutStockMapper = statutStockMapper;
        _stockMapper = stockMapper;
        _produitMapper = produitMapper;
        _familleProduitMapper = familleProduitMapper;
        _fournisseurMapper = fournisseurMapper;
        _mapper = mapper;
    }

    public void Ajouter(Domain.Entities.Stock nouveauStock)
    {
        var nouveauStockModel = _mapper.Map<StockModel>(nouveauStock);
        _context.Stocks.Add(nouveauStockModel);
        _context.SaveChanges();
    }

    public List<Domain.Entities.Stock> Lister()
    {
        var stocksModels = _context.Stocks
            .AsNoTracking()
            .Include(stock => stock.Produit).ThenInclude(produitModel => produitModel.FamilleProduit)
            .Include(stockModel => stockModel.Produit).ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(stockModel => stockModel.TransactionsStock)
            .ToList();

        var stocks = new List<Domain.Entities.Stock>();

        foreach (var stockModel in stocksModels)
        {
            var familleProduit = _familleProduitMapper.Mapper(stockModel.Produit.FamilleProduit);
            var fournisseur = _fournisseurMapper.Mapper(stockModel.Produit.Fournisseur);
            var produit = _produitMapper.Mapper(stockModel.Produit, familleProduit, fournisseur);
            var statutStock = _statutStockMapper.Mapper(stockModel.Statut);
            var stock = _stockMapper.Mapper(stockModel, produit, statutStock);

            stocks.Add(stock);
        }

        return stocks;
    }

    public Domain.Entities.Stock Trouver(int id)
    {
        var stockModel = _context.Stocks
            .AsNoTracking()
            .Include(stock => stock.Produit).ThenInclude(produitModel => produitModel.FamilleProduit)
            .Include(stockModel => stockModel.Produit).ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(stockModel => stockModel.TransactionsStock)
            .FirstOrDefault(stock => stock.Id == id);

        if (stockModel == null) throw new StockIntrouvable();

        var familleProduit = _familleProduitMapper.Mapper(stockModel.Produit.FamilleProduit);
        var fournisseur = _fournisseurMapper.Mapper(stockModel.Produit.Fournisseur);
        var produit = _produitMapper.Mapper(stockModel.Produit, familleProduit, fournisseur);
        var statutStock = _statutStockMapper.Mapper(stockModel.Statut);
        var stock = _stockMapper.Mapper(stockModel, produit, statutStock);

        return stock;
    }

    public Domain.Entities.Stock Trouver(string nom)
    {
        var stockModel = _context.Stocks
            .AsNoTracking()
            .Include(stock => stock.Produit).ThenInclude(produitModel => produitModel.FamilleProduit)
            .Include(stockModel => stockModel.Produit).ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(stockModel => stockModel.TransactionsStock)
            .FirstOrDefault(stock => stock.Produit.Nom == nom);

        if (stockModel == null) throw new StockIntrouvable();

        var familleProduit = _familleProduitMapper.Mapper(stockModel.Produit.FamilleProduit);
        var fournisseur = _fournisseurMapper.Mapper(stockModel.Produit.Fournisseur);
        var produit = _produitMapper.Mapper(stockModel.Produit, familleProduit, fournisseur);
        var statutStock = _statutStockMapper.Mapper(stockModel.Statut);
        var stock = _stockMapper.Mapper(stockModel, produit, statutStock);

        return stock;
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