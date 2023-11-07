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
        /// <param name="promotionRequestDTO"></param>
        /// <returns></returns>
        /// <response code="201">La promotion a été ajoutée avec succès</response>
        /// <response code="400">La promotion n'a pas pu être ajoutée</response>
        /// <response code="409">La promotion existe déjà</response>
        /// <response code="500">Erreur interne</response>
        [HttpPost("ajouter")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult AjouterUnePromotion([FromBody] PromotionRequestDTO promotionRequestDTO)
        {
            var response = _promotionService.AjouterUnePromotion(promotionRequestDTO);

            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Lister les promotions
        /// </summary>
        /// <returns> Liste des promotions </returns>
        /// <response code="200">Liste des promotions</response>
        /// <response code="500">Erreur interne</response>
        [HttpGet("lister")]
        [ProducesResponseType(typeof(PromotionResponseDTO), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult ListerLesPromotions()
        {
            var response = _promotionService.ListerLesPromotions();

            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
