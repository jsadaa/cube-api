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
        [ActionName("AjouterUnFournisseur")]
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
        [ActionName("ListerLesFournisseurs")]
        [ProducesResponseType(typeof(List<FournisseurResponseDTO>), 200)]
        [ProducesResponseType(typeof(string), 500)]
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
        /// <response code="200">Fournisseur</response>
        /// <response code="404">Fournisseur non trouvé</response>
        /// <response code="500">Erreur interne</response>
        [HttpGet("trouver/{id:int}")]
        [ActionName("TrouverUnFournisseurParId")]
        [ProducesResponseType(typeof(FournisseurResponseDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult TrouverUnFournisseurParId(int id)
        {
            var response = _fournisseurService.TrouverUnFournisseurParId(id);

            return StatusCode(response.StatusCode, response.Data);
        }
        
        /// <summary>
        /// Modifier un fournisseur
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fournisseurRequestDTO"></param>
        /// <returns></returns>
        /// <response code="200">Le fournisseur a été modifié avec succès</response>
        /// <response code="400">Le fournisseur n'a pas pu être modifié</response>
        /// <response code="404">Le fournisseur n'a pas été trouvé</response>
        /// <response code="500">Erreur interne</response>
        [HttpPut("modifier/{id:int}")]
        [ActionName("ModifierUnFournisseur")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult ModifierUnFournisseur(int id, [FromBody] FournisseurRequestDTO fournisseurRequestDTO)
        {
            var response = _fournisseurService.ModifierUnFournisseur(id, fournisseurRequestDTO);

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Supprimer un fournisseur
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Le fournisseur a été supprimé avec succès</response>
        /// <response code="400">Le fournisseur n'a pas pu être supprimé</response>
        /// <response code="404">Le fournisseur n'a pas été trouvé</response>
        /// <response code="500">Erreur interne</response>
        [HttpDelete("supprimer/{id:int}")]
        [ActionName("SupprimerUnFournisseur")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult SupprimerUnFournisseur(int id)
        {
            var response = _fournisseurService.SupprimerUnFournisseur(id);

            return StatusCode(response.StatusCode, response.Data);
        }
    }
}