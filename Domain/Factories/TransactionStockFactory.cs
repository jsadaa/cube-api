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
    
    public TransactionStock CreerTransactionStock(Produit produit, TypeTransactionStock type)
    {
        return new TransactionStock(
            quantite: 0,
            date: DateTime.Now,
            type: type,
            produit: produit,
            prixUnitaire: produit.PrixAchat
        );
    }
    
    
    public TransactionStock MapperTransactionStock(Produit produit, TransactionStockDTO transactionStock)
    {
        return new TransactionStock(
            id: transactionStock.Id,
            quantite: transactionStock.Quantite,
            date: transactionStock.Date,
            type: _typeTransactionStockMapper.Mapper(transactionStock.Type),
            produit: produit,
            prixUnitaire: transactionStock.PrixUnitaire
        );
    }

}