using ApiCube.Application.DTOs;
using ApiCube.Domain.Enums.Stock;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.TransactionStock;

public interface ITransactionStockMapper
{
    public Entities.TransactionStock Mapper(TransactionStockRequestDTO transactionStockDTO, Entities.Stock stock, TypeTransactionStock typeTransactionStock);
    public Entities.TransactionStock Mapper(TransactionStockModel transactionStockModel, Entities.Stock stock, TypeTransactionStock typeTransactionStock);
}