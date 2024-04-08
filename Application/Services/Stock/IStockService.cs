using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests.Stock;

namespace ApiCube.Application.Services.Stock;

public interface IStockService
{
    public BaseResponse AjouterUnStockDeProduit(StockRequest stockRequest);
    public BaseResponse ListerLesStocks();

    public BaseResponse TrouverUnStock(int id);
    public BaseResponse TrouverUnStockParProduit(int produitId);

    public BaseResponse ModifierUnStock(int id, StockUpdate stockUpdate);

    public BaseResponse SupprimerUnStock(int id);
    public BaseResponse ListerLesTransactionsStock();
    public BaseResponse ListerLesTransactionsStockParStock(int stockId);
}