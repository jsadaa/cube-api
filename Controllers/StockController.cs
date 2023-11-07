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

        /// <summary>
        /// Ajouter un stock de produit
        /// </summary>
        /// <param name="stockRequestDTO"></param>
        /// <returns></returns>
        /// <response code="201">Le stock de produit a été ajouté avec succès</response>
        /// <response code="400">Le stock de produit n'a pas pu être ajouté</response>
        /// <response code="409">Le stock de produit existe déjà</response>
        /// <response code="500">Erreur interne</response>
        [HttpPost("ajouter")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult AjouterUnStockDeProduit([FromBody] StockRequestDTO stockRequestDTO)
        {
            BaseResponse response = _stockService.AjouterUnStockDeProduit(stockRequestDTO);

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Lister les stocks
        /// </summary>
        /// <returns> Liste des stocks </returns>
        /// <response code="200">Liste des stocks</response>
        /// <response code="500">Erreur interne</response>
        [HttpGet("lister")]
        [ProducesResponseType(typeof(List<StockResponseDTO>), 200)]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult ListerLesStocks()
        {
            BaseResponse response = _stockService.ListerLesStocks();

            return StatusCode(response.StatusCode, response.Data);
        }
    }
}