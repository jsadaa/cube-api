using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;

namespace ApiCube.Application.Services.Stock;

public interface IStockService
{
    public BaseResponse AjouterUnStockDeProduit(StockRequestDTO stockRequestDTO);
    public BaseResponse ListerLesStocks();
}