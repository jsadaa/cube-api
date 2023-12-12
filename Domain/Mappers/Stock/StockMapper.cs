using ApiCube.Domain.Enums.Stock;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Stock;

public class StockMapper : IStockMapper
{
    public Entities.Stock Mapper(StockModel stockModel, Entities.Produit produit,
        List<Entities.TransactionStock> transactionsStock, StatutStock statutStock)
    {
        var stock = new Entities.Stock(
            stockModel.Id,
            stockModel.Quantite,
            stockModel.SeuilDisponibilite,
            statutStock,
            produit,
            transactionsStock,
            stockModel.DateCreation,
            stockModel.DateModification,
            stockModel.DateSuppression
        );

        return stock;
    }

    public Entities.Stock Mapper(StockModel stockModel, Entities.Produit produit, StatutStock statutStock)
    {
        var stock = new Entities.Stock(
            stockModel.Id,
            stockModel.Quantite,
            stockModel.SeuilDisponibilite,
            produit: produit,
            statut: statutStock,
            transactionStocks: new List<Entities.TransactionStock>(),
            dateCreation: stockModel.DateCreation,
            dateModification: stockModel.DateModification,
            dateSuppression: stockModel.DateSuppression
        );

        return stock;
    }
}