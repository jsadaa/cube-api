using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Factories;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.Stock;

namespace ApiCube.Application.Services.Stock;

public class StockService : IStockService
{
    private readonly IProduitRepository _produitRepository;
    private readonly IStockRepository _stockRepository;
    private readonly StockFactory _stockFactory;
    
    public StockService(IProduitRepository produitRepository, IStockRepository stockRepository, StockFactory stockFactory)
    {
        _produitRepository = produitRepository;
        _stockRepository = stockRepository;
        _stockFactory = stockFactory;
    }
}