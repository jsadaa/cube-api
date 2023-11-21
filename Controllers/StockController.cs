using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests.Stock;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.Stock;
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
        /// <response code="201">stock_ajoute</response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="409">stock_existe_deja</response>
        /// <response code="500">unexpected_error</response>
        [HttpPost("")]
        [ActionName("AjouterUnStockDeProduit")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult AjouterUnStockDeProduit([FromBody] StockRequest stockRequest)
        {
            var response = _stockService.AjouterUnStockDeProduit(stockRequest);
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Lister les stocks
        /// </summary>
        /// <returns> Liste des stocks </returns>
        /// <response code="200"></response>
        /// <response code="500">unexpected_error</response>
        [HttpGet("")]
        [ActionName("ListerLesStocks")]
        [ProducesResponseType(typeof(List<StockResponse>), 200)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult ListerLesStocks()
        {
            var response = _stockService.ListerLesStocks();
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Trouver un stock par son identifiant
        /// </summary>
        /// <param name="id">Identifiant du stock</param>
        /// <returns> Stock </returns>
        /// <response code="200"></response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="404">stock_introuvable</response>
        /// <response code="500">unexpected_error</response>
        [HttpGet("{id:int}")]
        [ActionName("TrouverUnStockParId")]
        [ProducesResponseType(typeof(StockResponse), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult TrouverUnStock(int id)
        {
            var response = _stockService.TrouverUnStock(id);
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Modifier un stock
        /// </summary>
        /// <param name="id">Identifiant du stock</param>
        /// <param name="stockUpdate"></param>
        /// <returns></returns>
        /// <response code="200">stock_modifie</response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="404">stock_introuvable</response>
        /// <response code="409">stock_existe_deja</response>
        /// <response code="500">erreur_interne</response>
        [HttpPut("{id:int}")]
        [ActionName("ModifierUnStock")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult ModifierUnStock(int id, [FromBody] StockUpdate stockUpdate)
        {
            var response = _stockService.ModifierUnStock(id, stockUpdate);
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Supprimer un stock
        /// </summary>
        /// <param name="id">Identifiant du stock</param>
        /// <returns></returns>
        /// <response code="200">stock_supprime</response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="404">stock_introuvable</response>
        /// <response code="500">erreur_interne</response>
        [HttpDelete("{id:int}")]
        [ActionName("SupprimerUnStock")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult SupprimerUnStock(int id)
        {
            var response = _stockService.SupprimerUnStock(id);
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}