using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Stock;

public class StockMapper : IStockMapper
{
    public Entities.Stock Mapper(StockModel stockModel, Entities.Produit produit,
        List<Entities.TransactionStock> transactionsStock)
    {
        var stock = new Entities.Stock(
            id: stockModel.Id,
            quantite: stockModel.Quantite,
            seuilDisponibilite: stockModel.SeuilDisponibilite,
            produit: produit,
            transactionStocks: transactionsStock,
            dateCreation: stockModel.DateCreation,
            datePeremption: stockModel.DatePeremption,
            dateModification: stockModel.DateModification,
            dateSuppression: stockModel.DateSuppression
        );

        return stock;
    }

    public Entities.Stock Mapper(StockModel stockModel, Entities.Produit produit)
    {
        var stock = new Entities.Stock(
            id: stockModel.Id,
            quantite: stockModel.Quantite,
            seuilDisponibilite: stockModel.SeuilDisponibilite,
            produit: produit,
            transactionStocks: new List<Entities.TransactionStock>(),
            dateCreation: stockModel.DateCreation,
            datePeremption: stockModel.DatePeremption,
            dateModification: stockModel.DateModification,
            dateSuppression: stockModel.DateSuppression
        );

        return stock;
    }
}