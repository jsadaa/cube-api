using ApiCube.Domain.Enums.Stock;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Stock;

public interface IStockMapper
{
    public Entities.Stock Mapper(StockModel stockModel, Entities.Produit produit,
        List<Entities.TransactionStock> transactionsStock, StatutStock statutStock);

    public Entities.Stock Mapper(StockModel stockModel, Entities.Produit produit, StatutStock statutStock);
}