using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests.Produit;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.Produit;
using Microsoft.AspNetCore.Authorization;
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
    /// <param name="produitRequest"></param>
    /// <returns></returns>
    /// <response code="201">Le produit a été ajouté avec succès</response>
    /// <response code="400">Le produit n'a pas pu être ajouté</response>
    /// <response code="409">Le produit existe déjà</response>
    /// <response code="500">Erreur interne</response>
    [HttpPost("")]
    [ActionName("AjouterUnProduitAuCatalogue")]
    [ProducesResponseType(typeof(string), 201)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 409)]
    [ProducesResponseType(typeof(string), 500)]
    [Produces("application/json")]
    public IActionResult AjouterUnProduitAuCatalogue([FromBody] ProduitRequest produitRequest)
    {
        BaseResponse response = _produitService.AjouterUnProduitAuCatalogue(produitRequest);

        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    /// Lister les produits
    /// </summary>
    /// <returns> Liste des produits </returns>
    /// <response code="200">Liste des produits</response>
    /// <response code="500">Erreur interne</response>
    [Authorize(Roles = "Client")]
    [HttpGet("")]
    [ActionName("ListerLesProduits")]
    [ProducesResponseType(typeof(List<ProduitResponse>), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [Produces("application/json")]
    public IActionResult ListerLesProduits()
    {
        BaseResponse response = _produitService.ListerLesProduits();

        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    /// Trouver un produit par son identifiant
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200"> Produit </response>
    /// <response code="404"> Produit non trouvé </response>
    /// <response code="500"> Erreur interne </response>
    [HttpGet("{id:int}")]
    [ActionName("TrouverUnProduitParId")]
    [ProducesResponseType(typeof(ProduitResponse), 200)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    [Produces("application/json")]
    public IActionResult TrouverUnProduit(int id)
    {
        BaseResponse response = _produitService.TrouverUnProduit(id);

        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    /// Modifier un produit
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produitUpdate"></param>
    /// <returns></returns>
    /// <response code="200">Le produit a été modifié avec succès</response>
    /// <response code="400">Le produit n'a pas pu être modifié</response>
    /// <response code="404">Le produit n'a pas été trouvé</response>
    /// <response code="500">Erreur interne</response>
    [HttpPut("{id:int}")]
    [ActionName("ModifierUnProduit")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    [Produces("application/json")]
    public IActionResult ModifierUnProduit(int id, [FromBody] ProduitUpdate produitUpdate)
    {
        BaseResponse response = _produitService.ModifierUnProduit(id, produitUpdate);

        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    /// Supprimer un produit
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Le produit a été supprimé avec succès</response>
    /// <response code="400">Le produit n'a pas pu être supprimé</response>
    /// <response code="404">Le produit n'a pas été trouvé</response>
    /// <response code="500">Erreur interne</response>
    [HttpDelete("{id:int}")]
    [ActionName("SupprimerUnProduit")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    [Produces("application/json")]
    public IActionResult SupprimerUnProduit(int id)
    {
        BaseResponse response = _produitService.SupprimerUnProduit(id);

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
    [HttpPut("{produitId:int}/promotion/{promotionId:int}")]
    [ActionName("AppliquerUnePromotionSurUnProduit")]
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
    [HttpPut("{produitId:int}/promotion")]
    [ActionName("SupprimerUnePromotionSurUnProduit")]
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