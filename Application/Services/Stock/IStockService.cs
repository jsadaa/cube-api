using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Application.Services.Stock;

public interface IStockService
{
    public BaseResponse AjouterUnStockDeProduit(AjouterStockRequest stockRequest);
    public BaseResponse ListerLesStocks();
}