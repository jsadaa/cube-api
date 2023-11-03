using ApiCube.DTOs.Responses;

namespace ApiCube.Repositories.TransactionStock;

public interface ITransactionStockRepository
{
    public int Ajouter(TransactionStockDTO transactionStock);
    public TransactionStockDTO? Trouver(int id);
    public List<TransactionStockDTO> Lister();
    public int? Modifier(int id, TransactionStockDTO transactionStock);
    public void Supprimer(int id);
}