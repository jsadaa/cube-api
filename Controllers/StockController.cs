using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests.Stock;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.Stock;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers
{
    [Route("api/stocks")]
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
        /// <param name="stockRequest"></param>
        /// <returns></returns>
        /// <response code="201">Le stock de produit a été ajouté avec succès</response>
        /// <response code="400">Le stock de produit n'a pas pu être ajouté</response>
        /// <response code="409">Le stock de produit existe déjà</response>
        /// <response code="500">Erreur interne</response>
        [HttpPost("")]
        [ActionName("AjouterUnStockDeProduit")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult AjouterUnStockDeProduit([FromBody] StockRequest stockRequest)
        {
            BaseResponse response = _stockService.AjouterUnStockDeProduit(stockRequest);

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Lister les stocks
        /// </summary>
        /// <returns> Liste des stocks </returns>
        /// <response code="200">Liste des stocks</response>
        /// <response code="500">Erreur interne</response>
        [HttpGet("")]
        [ActionName("ListerLesStocks")]
        [ProducesResponseType(typeof(List<StockResponse>), 200)]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult ListerLesStocks()
        {
            BaseResponse response = _stockService.ListerLesStocks();

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Trouver un stock par son identifiant
        /// </summary>
        /// <param name="id">Identifiant du stock</param>
        /// <returns> Stock </returns>
        /// <response code="200">Stock</response>
        /// <response code="404">Stock non trouvé</response>
        /// <response code="500">Erreur interne</response>
        [HttpGet("{id:int}")]
        [ActionName("TrouverUnStockParId")]
        [ProducesResponseType(typeof(StockResponse), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult TrouverUnStock(int id)
        {
            BaseResponse response = _stockService.TrouverUnStock(id);

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Modifier un stock
        /// </summary>
        /// <param name="id">Identifiant du stock</param>
        /// <param name="stockUpdate"></param>
        /// <returns></returns>
        /// <response code="200">Le stock a été modifié avec succès</response>
        /// <response code="400">Le stock n'a pas pu être modifié</response>
        /// <response code="404">Le stock n'a pas été trouvé</response>
        /// <response code="500">Erreur interne</response>
        [HttpPut("{id:int}")]
        [ActionName("ModifierUnStock")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult ModifierUnStock(int id, [FromBody] StockUpdate stockUpdate)
        {
            BaseResponse response = _stockService.ModifierUnStock(id, stockUpdate);

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Supprimer un stock
        /// </summary>
        /// <param name="id">Identifiant du stock</param>
        /// <returns></returns>
        /// <response code="200">Le stock a été supprimé avec succès</response>
        /// <response code="400">Le stock n'a pas pu être supprimé</response>
        /// <response code="404">Le stock n'a pas été trouvé</response>
        /// <response code="500">Erreur interne</response>
        [HttpDelete("{id:int}")]
        [ActionName("SupprimerUnStock")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult SupprimerUnStock(int id)
        {
            BaseResponse response = _stockService.SupprimerUnStock(id);

            return StatusCode(response.StatusCode, response.Data);
        }
    }
}