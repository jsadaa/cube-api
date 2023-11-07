using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.Fournisseur;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers
{
    [Route("api/fournisseurs")]
    [ApiController]
    public class FournisseurController : ControllerBase
    {
        private readonly IFournisseurService _fournisseurService;

        public FournisseurController(IFournisseurService fournisseurService)
        {
            _fournisseurService = fournisseurService;
        }

        [HttpPost("ajouter")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult AjouterUnFournisseur([FromBody] FournisseurRequestDTO fournisseurRequestDTO)
        {
            var response = _fournisseurService.AjouterUnFournisseur(fournisseurRequestDTO);

            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpGet("lister")]
        [ProducesResponseType(typeof(List<FournisseurResponseDTO>), 200)]
        public IActionResult ListerLesFournisseurs()
        {
            var response = _fournisseurService.ListerLesFournisseurs();

            return StatusCode(response.StatusCode, response.Data);
        }
    }
}