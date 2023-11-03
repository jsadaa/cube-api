using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Persistence.Repositories.TransactionStock;

public interface ITransactionStockRepository
{
    public int Ajouter(TransactionStockDTO transactionStock);
    public TransactionStockDTO? Trouver(int id);
    public List<TransactionStockDTO> Lister();
    public List<TransactionStockDTO> ListerParStock(int idStock);
    public int? Modifier(int id, TransactionStockDTO transactionStock);
    public void Supprimer(int id);
}