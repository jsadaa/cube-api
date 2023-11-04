using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.FamilleProduit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers
{
    [Route("api/familleproduits")]
    [ApiController]
    public class FamilleProduitController : ControllerBase
    {
        private readonly IFamilleProduitService _familleProduitService;
        
        public FamilleProduitController(IFamilleProduitService familleProduitService)
        {
            _familleProduitService = familleProduitService;
        }
        
        [HttpPost("ajouter")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult AjouterUneFamilleProduit([FromBody] FamilleProduitRequestDTO familleProduitRequestDTO)
        {
            BaseResponse response = _familleProduitService.AjouterUneFamilleProduit(familleProduitRequestDTO);
            
            return StatusCode(response.StatusCode, response.Data);
        }
        
        [HttpGet("lister")]
        [ProducesResponseType(typeof(List<FamilleProduitResponseDTO>), 200)]
        public IActionResult ListerLesFamillesProduits()
        {
            BaseResponse response = _familleProduitService.ListerLesFamillesProduits();
            
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
