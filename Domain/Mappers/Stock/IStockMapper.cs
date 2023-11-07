using ApiCube.Application.DTOs.Requests;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Stock;

public interface IStockMapper
{
    public Entities.Stock Mapper(StockModel stockModel, Entities.Produit produit,
        List<Entities.TransactionStock> transactionsStock);

    public Entities.Stock Mapper(StockModel stockModel, Entities.Produit produit);
}