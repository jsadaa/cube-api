using ApiCube.Application.DTOs;
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
        /// <param name="fournisseurRequest"></param>
        /// <returns></returns>
        /// <response code="201">fournisseur_ajoute</response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="409">fournisseur_existe_deja</response>
        /// <response code="500">unexpected_error</response>
        [HttpPost("")]
        [ActionName("AjouterUnFournisseur")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult AjouterUnFournisseur([FromBody] FournisseurRequest fournisseurRequest)
        {
            var response = _fournisseurService.AjouterUnFournisseur(fournisseurRequest);

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Lister les fournisseurs
        /// </summary>
        /// <returns> Liste des fournisseurs </returns>
        /// <response code="200"></response>
        /// <response code="500">unexpected_error</response>
        [HttpGet("")]
        [ActionName("ListerLesFournisseurs")]
        [ProducesResponseType(typeof(List<FournisseurResponse>), 200)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult ListerLesFournisseurs()
        {
            var response = _fournisseurService.ListerLesFournisseurs();
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Trouver un fournisseur par son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Fournisseur </returns>
        /// <response code="200"></response>
        /// <response code="404">fournisseur_introuvable</response>
        /// <response code="500">unexpected_error</response>
        [HttpGet("{id:int}")]
        [ActionName("TrouverUnFournisseurParId")]
        [ProducesResponseType(typeof(FournisseurResponse), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult TrouverUnFournisseur(int id)
        {
            var response = _fournisseurService.TrouverUnFournisseur(id);
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Modifier un fournisseur
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fournisseurRequest"></param>
        /// <returns></returns>
        /// <response code="200">fournisseur_modifie</response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="404">fournisseur_introuvable</response>
        /// <response code="409">fournisseur_existe_deja</response>
        /// <response code="500">Erreur interne</response>
        [HttpPut("{id:int}")]
        [ActionName("ModifierUnFournisseur")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult ModifierUnFournisseur(int id, [FromBody] FournisseurRequest fournisseurRequest)
        {
            var response = _fournisseurService.ModifierUnFournisseur(id, fournisseurRequest);
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Supprimer un fournisseur
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">fournisseur_supprime</response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="404">fournisseur_introuvable</response>
        /// <response code="500">unexpected_error</response>
        [HttpDelete("{id:int}")]
        [ActionName("SupprimerUnFournisseur")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult SupprimerUnFournisseur(int id)
        {
            var response = _fournisseurService.SupprimerUnFournisseur(id);
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}