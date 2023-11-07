using ApiCube.Application.DTOs.Requests;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Stock;

public interface IStockMapper
{
    public Entities.Stock Mapper(StockRequestDTO stockRequestDTO, Entities.Produit produit, List<Entities.TransactionStock> transactionsStock);
    public Entities.Stock Mapper(StockModel stockModel, Entities.Produit produit, List<Entities.TransactionStock> transactionsStock);
    public Entities.Stock MapperSansTransactionsStock(StockRequestDTO stockRequestDTO, Entities.Produit produit);
    public Entities.Stock MapperSansTransactionsStock(StockModel stockModel, Entities.Produit produit);
}