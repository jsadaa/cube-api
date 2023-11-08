using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.FamilleProduit;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers
{
    [Route("api/famillesproduits")]
    [ApiController]
    public class FamilleProduitController : ControllerBase
    {
        private readonly IFamilleProduitService _familleProduitService;

        public FamilleProduitController(IFamilleProduitService familleProduitService)
        {
            _familleProduitService = familleProduitService;
        }

        /// <summary>
        /// Ajouter une famille de produit
        /// </summary>
        /// <param name="familleProduitRequestDTO"></param>
        /// <returns></returns>
        /// <response code="201">La famille de produit a été ajoutée avec succès</response>
        /// <response code="400">La famille de produit n'a pas pu être ajoutée</response>
        /// <response code="409">La famille de produit existe déjà</response>
        /// <response code="500">Erreur interne</response>
        [HttpPost("ajouter")]
        [ActionName("AjouterUneFamilleProduit")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult AjouterUneFamilleProduit([FromBody] FamilleProduitRequestDTO familleProduitRequestDTO)
        {
            var response = _familleProduitService.AjouterUneFamilleProduit(familleProduitRequestDTO);

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Lister les familles de produits
        /// </summary>
        /// <returns> Liste des familles de produits </returns>
        /// <response code="200">Liste des familles de produits</response>
        /// <response code="500">Erreur interne</response>
        [HttpGet("lister")]
        [ActionName("ListerLesFamillesProduits")]
        [ProducesResponseType(typeof(List<FamilleProduitResponseDTO>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult ListerLesFamillesProduits()
        {
            var response = _familleProduitService.ListerLesFamillesProduits();

            return StatusCode(response.StatusCode, response.Data);
        }
        
        /// <summary>
        /// Trouver une famille de produit par son identifiant
        /// </summary>
        /// <param name="id">Identifiant de la famille de produit</param>
        /// <returns> Famille de produit </returns>
        /// <response code="200">Famille de produit</response>
        /// <response code="404">Famille de produit non trouvée</response>
        /// <response code="500">Erreur interne</response>
        [HttpGet("trouver/{id:int}")]
        [ActionName("TrouverUneFamilleProduit")]
        [ProducesResponseType(typeof(FamilleProduitResponseDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult TrouverUneFamilleProduit(int id)
        {
            var response = _familleProduitService.TrouverUneFamilleProduit(id);

            return StatusCode(response.StatusCode, response.Data);
        }
        
    }
}