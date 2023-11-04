using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.FamilleProduit;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers
{
    [Route("api/familles_produits")]
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
            var response = _familleProduitService.AjouterUneFamilleProduit(familleProduitRequestDTO);
            
            return StatusCode(response.StatusCode, response.Data);
        }
        
        [HttpGet("lister")]
        [ProducesResponseType(typeof(List<FamilleProduitResponseDTO>), 200)]
        public IActionResult ListerLesFamillesProduits()
        {
            var response = _familleProduitService.ListerLesFamillesProduits();
            
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
