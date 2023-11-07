using ApiCube.Domain.Enums.Stock;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.TransactionStock;

public class TransactionStockMapper : ITransactionStockMapper
{
    public Entities.TransactionStock Mapper(TransactionStockModel transactionStockModel, Entities.Stock stock,
        TypeTransactionStock typeTransactionStock)
    {
        return new Entities.TransactionStock(
            id: transactionStockModel.Id,
            quantite: transactionStockModel.Quantite,
            date: transactionStockModel.Date,
            type: typeTransactionStock,
            stock: stock,
            prixUnitaire: transactionStockModel.PrixUnitaire,
            quantiteAvant: transactionStockModel.QuantiteAvant,
            quantiteApres: transactionStockModel.QuantiteApres
        );
    }
}