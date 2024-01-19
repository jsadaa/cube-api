using ApiCube.Domain.Enums.Stock;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.TransactionStock;

public class TransactionStockMapper : ITransactionStockMapper
{
    public Entities.TransactionStock Mapper(TransactionStockModel transactionStockModel, Entities.Stock stock,
        TypeTransactionStock typeTransactionStock)
    {
        return new Entities.TransactionStock(
            transactionStockModel.Id,
            transactionStockModel.VariationQuantite,
            transactionStockModel.Date,
            typeTransactionStock,
            stock,
            transactionStockModel.PrixUnitaire,
            transactionStockModel.QuantiteAvant,
            transactionStockModel.QuantiteApres
        );
    }
}