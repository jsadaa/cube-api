using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Persistence.Repositories.Stock;

public interface IStockRepository
{
    public int Ajouter(AjouterStockRequest stock);
    public List<StockDTO> Lister();
    public StockDTO? Trouver(int id);
    public int? Modifier(int id, AjouterStockRequest stock);
    public void Supprimer(int id);
}