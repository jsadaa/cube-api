using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Stock;

namespace ApiCube.Domain.Factories;

public class TransactionStockFactory
{
    private readonly TypeTransactionStockMapper _typeTransactionStockMapper;
    
    public TransactionStockFactory(TypeTransactionStockMapper typeTransactionStockMapper)
    {
        _typeTransactionStockMapper = typeTransactionStockMapper;
    }
    
    public TransactionStock CreerTransactionStock(Stock stock, TypeTransactionStock type)
    {
        return new TransactionStock(
            quantite: stock.Quantite,
            date: DateTime.Now,
            type: type,
            stock: stock,
            produit: stock.Produit,
            prixUnitaire: stock.Produit.PrixAchat
        );
    }
    
    public TransactionStock MapperTransactionStock(Stock stock, TransactionStockDTO transactionStock)
    {
        return new TransactionStock(
            id: transactionStock.Id,
            quantite: transactionStock.Quantite,
            date: transactionStock.Date,
            type: _typeTransactionStockMapper.Mapper(transactionStock.Type),
            stock: stock,
            produit: stock.Produit,
            prixUnitaire: transactionStock.PrixUnitaire
        );
    }

}