using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.Promotion;
using ApiCube.Persistence.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers
{
    [Route("api/promotions")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        /// <summary>
        /// Ajouter une promotion
        /// </summary>
        /// <param name="promotionRequest"></param>
        /// <returns></returns>
        /// <response code="201">La promotion a été ajoutée avec succès</response>
        /// <response code="400">La promotion n'a pas pu être ajoutée</response>
        /// <response code="409">La promotion existe déjà</response>
        /// <response code="500">Erreur interne</response>
        [HttpPost("")]
        [ActionName("AjouterUnePromotion")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult AjouterUnePromotion([FromBody] PromotionRequest promotionRequest)
        {
            var response = _promotionService.AjouterUnePromotion(promotionRequest);

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Lister les promotions
        /// </summary>
        /// <returns> Liste des promotions </returns>
        /// <response code="200">Liste des promotions</response>
        /// <response code="500">Erreur interne</response>
        [HttpGet("")]
        [ActionName("ListerLesPromotions")]
        [ProducesResponseType(typeof(PromotionResponse), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult ListerLesPromotions()
        {
            var response = _promotionService.ListerLesPromotions();

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Trouver une promotion par son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Promotion </returns>
        /// <response code="200">Promotion</response>
        /// <response code="404">Promotion non trouvée</response>
        /// <response code="500">Erreur interne</response>
        /// <response code="400">Id invalide</response>
        [HttpGet("{id:int}")]
        [ActionName("TrouverUnePromotionParId")]
        [ProducesResponseType(typeof(PromotionResponse), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 400)]
        [Produces("application/json")]
        public IActionResult TrouverUnePromotion(int id)
        {
            var response = _promotionService.TrouverUnePromotion(id);

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Modifier une promotion
        /// </summary>
        /// <param name="id"></param>
        /// <param name="promotionRequest"></param>
        /// <returns></returns>
        /// <response code="200">La promotion a été modifiée avec succès</response>
        /// <response code="400">Id invalide</response>
        /// <response code="404">La promotion n'a pas été trouvée</response>
        /// <response code="409">La promotion existe déjà</response>
        /// <response code="500">Erreur interne</response>
        [HttpPut("{id:int}")]
        [ActionName("ModifierUnePromotion")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult ModifierUnePromotion(int id, [FromBody] PromotionRequest promotionRequest)
        {
            var response = _promotionService.ModifierUnePromotion(id, promotionRequest);

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Supprimer une promotion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">La promotion a été supprimée avec succès</response>
        /// <response code="400">Id invalide</response>
        /// <response code="404">La promotion n'a pas été trouvée</response>
        /// <response code="500">Erreur interne</response>
        [HttpDelete("{id:int}")]
        [ActionName("SupprimerUnePromotion")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult SupprimerUnePromotion(int id)
        {
            var response = _promotionService.SupprimerUnePromotion(id);

            return StatusCode(response.StatusCode, response.Data);
        }
    }
}