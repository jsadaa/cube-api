using System.Net;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Stock;
using ApiCube.Domain.Factories;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.Stock;
using ApiCube.Persistence.Repositories.TransactionStock;

namespace ApiCube.Application.Services.Stock;

public class StockService : IStockService
{
    private readonly IProduitRepository _produitRepository;
    private readonly ITransactionStockRepository _transactionStockRepository;
    private readonly IStockRepository _stockRepository;
    private readonly StockFactory _stockFactory;
    private readonly TransactionStockFactory _transactionStockFactory;
    
    public StockService(
        IProduitRepository produitRepository,
        ITransactionStockRepository transactionStockRepository, 
        IStockRepository stockRepository, 
        StockFactory stockFactory,
        TransactionStockFactory transactionStockFactory
        )
    {
        _produitRepository = produitRepository;
        _transactionStockRepository = transactionStockRepository;
        _stockRepository = stockRepository;
        _stockFactory = stockFactory;
        _transactionStockFactory = transactionStockFactory;
    }
    
    public BaseResponse AjouterUnStockDeProduit(AjouterStockRequest stockRequest)
    {
        try
        {
            Domain.Entities.Stock nouveauStock = _stockFactory.CréerStock(stockRequest);
            TransactionStock transactionStock = _transactionStockFactory.CreerTransactionStock(
                nouveauStock, 
                TypeTransactionStock.Achat
            );
            
            nouveauStock.AjouterTransaction(transactionStock);
            transactionStock.Stock.Id = _stockRepository.Ajouter(nouveauStock.ToRequestDTO());
            _transactionStockRepository.Ajouter(transactionStock.ToDTO());
            
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Stock ajouté avec succès" }
            );
            
            return response;
        }
        catch (Exception e)
        {
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );
            
            return response;
        }
    }
    
    public BaseResponse ListerLesStocks()
    {
        try
        {
            List<StockDTO> stocks = _stockRepository.Lister();
            
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: stocks
            );
            
            return response;
        }
        catch (Exception e)
        {
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );
            
            return response;
        }
    }
}