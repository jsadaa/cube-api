using ApiCube.Domain.Entities;
using ApiCube.DTOs.Responses;

namespace ApiCube.Repositories.Interfaces;

public interface ITransactionStockRepository
{
    public void Ajouter(TransactionStockDTO transactionStock);
    public TransactionStockDTO? Trouver(int id);
    public List<TransactionStockDTO> Lister();
    public void Modifier(int id, TransactionStockDTO transactionStock);
    public void Supprimer(int id);
}