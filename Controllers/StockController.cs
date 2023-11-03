using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.Stock;
using Microsoft.AspNetCore.Http;
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
        public IActionResult AjouterUnStockDeProduit([FromBody] AjouterStockRequest ajouterStock)
        {
            BaseResponse response = _stockService.AjouterUnStockDeProduit(ajouterStock);
            
            return StatusCode(response.StatusCode, response.Data);
        }
        
        [HttpGet("lister")]
        [ProducesResponseType(typeof(List<StockDTO>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult ListerLesStocks()
        {
            BaseResponse response = _stockService.ListerLesStocks();
            
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
