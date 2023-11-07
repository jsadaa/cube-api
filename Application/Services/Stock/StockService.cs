using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Stock;
using ApiCube.Domain.Mappers.Stock;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.Stock;
using AutoMapper;

namespace ApiCube.Application.Services.Stock;

public class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;
    private readonly IProduitRepository _produitRepository;
    private readonly IMapper _mapper;
    private readonly IStockMapper _stockMapper;
    
    public StockService(
        IStockRepository stockRepository, 
        IProduitRepository produitRepository,
        IMapper mapper,
        IStockMapper stockMapper
    )
    {
        _stockRepository = stockRepository;
        _stockMapper = stockMapper;
        _produitRepository = produitRepository;
        _mapper = mapper;
    }
    
    public BaseResponse AjouterUnStockDeProduit(StockRequestDTO stockRequestDTO)
    {
        try
        {
            var produit = _produitRepository.Trouver(stockRequestDTO.ProduitId);
            var nouveauStock = _stockMapper.MapperSansTransactionsStock(stockRequestDTO, produit);
            
            // Ajout d'une transaction stock pour l'achat initial
            var nouvelleTransactionStock = new TransactionStock(
                quantite: stockRequestDTO.Quantite,
                date: DateTime.Now,
                type: TypeTransactionStock.Achat,
                stock: nouveauStock,
                prixUnitaire: produit.PrixAchat,
                quantiteAvant: 0,
                quantiteApres: stockRequestDTO.Quantite
            );
            
            nouveauStock.AjouterTransaction(nouvelleTransactionStock);
            _stockRepository.Ajouter(nouveauStock);
            
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
                data: stocks
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