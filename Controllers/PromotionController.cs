using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.Promotion;
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
        /// <response code="201">promotion_ajoute</response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="409">promotion_existe_deja</response>
        /// <response code="500">unexpected_error</response>
        [HttpPost("")]
        [ActionName("AjouterUnePromotion")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
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
        /// <response code="200"></response>
        /// <response code="500">unexpected_error</response>
        [HttpGet("")]
        [ActionName("ListerLesPromotions")]
        [ProducesResponseType(typeof(PromotionResponse), 200)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
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
        /// <response code="200"></response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="404">promotion_introuvable</response>
        /// <response code="500">erreur_interne</response>
        [HttpGet("{id:int}")]
        [ActionName("TrouverUnePromotionParId")]
        [ProducesResponseType(typeof(PromotionResponse), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
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
        /// <response code="200">promotion_modifiee</response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="404">promotion_introuvable</response>
        /// <response code="409">promotion_existe_deja</response>
        /// <response code="500">unexpected_error</response>
        [HttpPut("{id:int}")]
        [ActionName("ModifierUnePromotion")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
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
        /// <response code="200">promotion_supprimee</response>
        /// <response code="400">donnees_invalides</response>
        /// <response code="404">promotion_introuvable</response>
        /// <response code="500">unexpected_error</response>
        [HttpDelete("{id:int}")]
        [ActionName("SupprimerUnePromotion")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult SupprimerUnePromotion(int id)
        {
            var response = _promotionService.SupprimerUnePromotion(id);
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}