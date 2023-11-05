using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Stock;
using ApiCube.Persistence.Models;
using ApiCube.Persistence.Repositories.Stock;

namespace ApiCube.Domain.Factories;

public class TransactionStockFactory
{
    private readonly TypeTransactionStockMapper _typeTransactionStockMapper;
    private readonly IStockRepository _stockRepository;
    
    public TransactionStockFactory(TypeTransactionStockMapper typeTransactionStockMapper, IStockRepository stockRepository)
    {
        _typeTransactionStockMapper = typeTransactionStockMapper;
        _stockRepository = stockRepository;
    }
    
    public TransactionStock Creer(TransactionStockInnerDTO transactionStockInnerDTO)
    {
        Stock stock = _stockRepository.Trouver(transactionStockInnerDTO.StockId);
        
        return new TransactionStock(
            quantite: transactionStockInnerDTO.Quantite,
            date: transactionStockInnerDTO.Date,
            type: _typeTransactionStockMapper.Mapper(transactionStockInnerDTO.Type),
            stock: stock,
            produit: stock.Produit,
            prixUnitaire: transactionStockInnerDTO.PrixUnitaire,
            quantiteAvant: transactionStockInnerDTO.QuantiteAvant,
            quantiteApres: transactionStockInnerDTO.QuantiteApres
        );
    }
    
    public TransactionStock Mapper(TransactionStockModel transactionStockModel)
    {
        Stock stock = _stockRepository.Trouver(transactionStockModel.StockId);
        
        return new TransactionStock(
            id: transactionStockModel.Id,
            quantite: transactionStockModel.Quantite,
            date: transactionStockModel.Date,
            type: _typeTransactionStockMapper.Mapper(transactionStockModel.Type),
            stock: stock,
            produit: stock.Produit,
            prixUnitaire: transactionStockModel.PrixUnitaire,
            quantiteAvant: transactionStockModel.QuantiteAvant,
            quantiteApres: transactionStockModel.QuantiteApres
        );
    }

}