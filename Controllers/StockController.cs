using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.Stock;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost("ajouter")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult AjouterUnStockDeProduit([FromBody] StockRequestDTO stockRequestDTO)
        {
            BaseResponse response = _stockService.AjouterUnStockDeProduit(stockRequestDTO);

            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpGet("lister")]
        [ProducesResponseType(typeof(List<StockResponseDTO>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult ListerLesStocks()
        {
            BaseResponse response = _stockService.ListerLesStocks();

            return StatusCode(response.StatusCode, response.Data);
        }
    }
}