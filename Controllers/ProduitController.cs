using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.Produit;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers;

[Route("api/produits")]
[ApiController]
public class ProduitController : ControllerBase
{
    private readonly IProduitService _produitService;

    public ProduitController(IProduitService produitService)
    {
        _produitService = produitService;
    }

    /// <summary>
    /// Ajouter un produit au catalogue
    /// </summary>
    /// <param name="produitRequestDTO"></param>
    /// <returns></returns>
    /// <response code="201">Le produit a été ajouté avec succès</response>
    /// <response code="400">Le produit n'a pas pu être ajouté</response>
    /// <response code="409">Le produit existe déjà</response>
    /// <response code="500">Erreur interne</response>
    [HttpPost("ajouter")]
    [ProducesResponseType(typeof(string), 201)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 409)]
    [ProducesResponseType(typeof(string), 500)]
    [Produces("application/json")]
    public IActionResult AjouterUnProduitAuCatalogue([FromBody] ProduitRequestDTO produitRequestDTO)
    {
        BaseResponse response = _produitService.AjouterUnProduitAuCatalogue(produitRequestDTO);

        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    /// Lister les produits
    /// </summary>
    /// <returns> Liste des produits </returns>
    /// <response code="200">Liste des produits</response>
    /// <response code="500">Erreur interne</response>
    [HttpGet("lister")]
    [ProducesResponseType(typeof(List<ProduitResponseDTO>), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [Produces("application/json")]
    public IActionResult ListerLesProduits()
    {
        BaseResponse response = _produitService.ListerLesProduits();

        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    /// Appliquer une promotion sur un produit
    /// </summary>
    /// <param name="produitId"></param>
    /// <param name="promotionId"></param>
    /// <returns></returns>
    /// <response code="200">La promotion a été appliquée avec succès</response>
    /// <response code="400">La promotion n'a pas pu être appliquée</response>
    /// <response code="404">Le produit n'a pas été trouvé</response>
    /// <response code="500">Erreur interne</response>
    [HttpPut("{produitId:int}/appliquer-promotion/{promotionId:int}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    [Produces("application/json")]
    public IActionResult AppliquerUnePromotionSurUnProduit(int produitId, int promotionId)
    {
        BaseResponse response = _produitService.AppliquerUnePromotion(produitId, promotionId);

        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    /// Supprimer une promotion d'un produit
    /// </summary>
    /// <param name="produitId"></param>
    /// <returns></returns>
    /// <response code="200">La promotion a été supprimée avec succès</response>
    /// <response code="400">La promotion n'a pas pu être supprimée</response>
    /// <response code="404">Le produit n'a pas été trouvé</response>
    /// <response code="500">Erreur interne</response>
    [HttpPut("{produitId:int}/supprimer-promotion")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    [Produces("application/json")]
    public IActionResult RetirerUnePromotionSurUnProduit(int produitId)
    {
        BaseResponse response = _produitService.RetirerUnePromotion(produitId);

        return StatusCode(response.StatusCode, response.Data);
    }
}