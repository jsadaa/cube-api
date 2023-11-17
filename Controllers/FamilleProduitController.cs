using ApiCube.Application.DTOs;
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
        /// <param name="familleProduitRequest"></param>
        /// <returns></returns>
        /// <response code="201">famille_produit_ajoute</response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="409">famille_produit_existe_deja</response>
        /// <response code="500">unexpected_error</response>
        [HttpPost("")]
        [ActionName("AjouterUneFamilleProduit")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult AjouterUneFamilleProduit([FromBody] FamilleProduitRequest familleProduitRequest)
        {
            var response = _familleProduitService.AjouterUneFamilleProduit(familleProduitRequest);
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Lister les familles de produits
        /// </summary>
        /// <returns> Liste des familles de produits </returns>
        /// <response code="200"></response>
        /// <response code="500">unexpected_error</response>
        [HttpGet("")]
        [ActionName("ListerLesFamillesProduits")]
        [ProducesResponseType(typeof(List<FamilleProduitResponse>), 200)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
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
        /// <response code="200"></response>
        /// <response code="404">famille_produit_introuvable</response>
        /// <response code="500">unexpected_error</response>
        [HttpGet("{id:int}")]
        [ActionName("TrouverUneFamilleProduitParId")]
        [ProducesResponseType(typeof(FamilleProduitResponse), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult TrouverUneFamilleProduit(int id)
        {
            var response = _familleProduitService.TrouverUneFamilleProduit(id);
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Modifier une famille de produit
        /// </summary>
        /// <param name="id">Identifiant de la famille de produit</param>
        /// <param name="familleProduitRequest"></param>
        /// <returns></returns>
        /// <response code="200">famille_produit_modifiee</response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="404">famille_produit_introuvable</response>
        /// <response code="409">famille_produit_existe_deja</response>
        /// <response code="500">unexpected_error</response>
        [HttpPut("{id:int}")]
        [ActionName("ModifierUneFamilleProduit")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult ModifierUneFamilleProduit(int id, [FromBody] FamilleProduitRequest familleProduitRequest)
        {
            var response = _familleProduitService.ModifierUneFamilleProduit(id, familleProduitRequest);
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Supprimer une famille de produit
        /// </summary>
        /// <param name="id">Identifiant de la famille de produit</param>
        /// <returns></returns>
        /// <response code="200">famille_produit_supprimee</response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="404">famille_produit_introuvable</response>
        /// <response code="500">unexpected_error</response>
        [HttpDelete("{id:int}")]
        [ActionName("SupprimerUneFamilleProduit")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult SupprimerUneFamilleProduit(int id)
        {
            var response = _familleProduitService.SupprimerUneFamilleProduit(id);
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}