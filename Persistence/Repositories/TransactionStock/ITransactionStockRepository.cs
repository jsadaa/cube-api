using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Persistence.Repositories.TransactionStock;

public interface ITransactionStockRepository
{
    public int Ajouter(Domain.Entities.TransactionStock nouveauTransactionStock);
    public Domain.Entities.TransactionStock Trouver(int id);
    public Domain.Entities.TransactionStock Trouver(string nom);
    public List<Domain.Entities.TransactionStock> Lister();
    public List<Domain.Entities.TransactionStock> ListerParStock(int stockId);
    public void Modifier(Domain.Entities.TransactionStock transactionStock);
    public void Supprimer(Domain.Entities.TransactionStock transactionStock);
}