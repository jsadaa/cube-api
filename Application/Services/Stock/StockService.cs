using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Enums.Stock;
using ApiCube.Domain.Factories;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.Stock;
using ApiCube.Persistence.Repositories.TransactionStock;
using AutoMapper;

namespace ApiCube.Application.Services.Stock;

public class StockService : IStockService
{
    private readonly IProduitRepository _produitRepository;
    private readonly ITransactionStockRepository _transactionStockRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;
    private readonly StockFactory _stockFactory;
    private readonly TransactionStockFactory _transactionStockFactory;
    
    public StockService(
        IProduitRepository produitRepository, 
        ITransactionStockRepository transactionStockRepository, 
        IStockRepository stockRepository, 
        IMapper mapper,
        StockFactory stockFactory, 
        TransactionStockFactory transactionStockFactory
    )
    {
        _produitRepository = produitRepository;
        _transactionStockRepository = transactionStockRepository;
        _stockRepository = stockRepository;
        _stockFactory = stockFactory;
        _transactionStockFactory = transactionStockFactory;
        _mapper = mapper;
    }
    
    public BaseResponse AjouterUnStockDeProduit(StockRequestDTO stockRequestDTO)
    {
        try
        {
            var nouveauStock = _stockFactory.Creer(stockRequestDTO);
            var stockId = _stockRepository.Ajouter(nouveauStock);
            
            var nouvelleTransactionStock = _transactionStockFactory.Creer(
                new TransactionStockInnerDTO
            {
                Quantite = nouveauStock.Quantite,
                Date = DateTime.Now,
                Type = TypeTransactionStock.Achat.ToString(),
                ProduitId = nouveauStock.Produit.Id,
                StockId = stockId,
                PrixUnitaire = nouveauStock.Produit.PrixAchat,
                PrixTotal = nouveauStock.Produit.PrixAchat * nouveauStock.Quantite,
                QuantiteAvant = 0,
                QuantiteApres = nouveauStock.Quantite
            });
            _transactionStockRepository.Ajouter(nouvelleTransactionStock);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Stock ajouté avec succès" }
            );
            
            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
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
            var listeStocks = _stockRepository.Lister();
            var stocks = _mapper.Map<List<StockResponseDTO>>(listeStocks);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { stocks }
            );
            
            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );
            
            return response;
        }
    }
}