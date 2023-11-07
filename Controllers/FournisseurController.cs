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

        /// <summary>
        /// Ajouter un fournisseur
        /// </summary>
        /// <param name="fournisseurRequestDTO"></param>
        /// <returns></returns>
        /// <response code="201">Le fournisseur a été ajouté avec succès</response>
        /// <response code="400">Le fournisseur n'a pas pu être ajouté</response>
        /// <response code="409">Le fournisseur existe déjà</response>
        /// <response code="500">Erreur interne</response>
        [HttpPost("ajouter")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult AjouterUnFournisseur([FromBody] FournisseurRequestDTO fournisseurRequestDTO)
        {
            var response = _fournisseurService.AjouterUnFournisseur(fournisseurRequestDTO);

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Lister les fournisseurs
        /// </summary>
        /// <returns> Liste des fournisseurs </returns>
        /// <response code="200">Liste des fournisseurs</response>
        /// <response code="500">Erreur interne</response>
        [HttpGet("lister")]
        [ProducesResponseType(typeof(List<FournisseurResponseDTO>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult ListerLesFournisseurs()
        {
            var response = _fournisseurService.ListerLesFournisseurs();

            return StatusCode(response.StatusCode, response.Data);
        }
    }
}