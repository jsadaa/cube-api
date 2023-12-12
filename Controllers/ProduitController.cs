using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests.Produit;
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
    ///     Ajouter un produit au catalogue
    /// </summary>
    /// <param name="produitRequest"></param>
    /// <returns></returns>
    /// <response code="201">produit_ajouté</response>
    /// <response code="400">donnees_invalides</response>
    /// <response code="409">produit_existe_deja</response>
    /// <response code="500">unexpected_error</response>
    [HttpPost("")]
    [ActionName("AjouterUnProduitAuCatalogue")]
    [ProducesResponseType(typeof(string), 201)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult AjouterUnProduitAuCatalogue([FromBody] ProduitRequest produitRequest)
    {
        var response = _produitService.AjouterUnProduitAuCatalogue(produitRequest);

        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Lister les produits
    /// </summary>
    /// <returns> Liste des produits </returns>
    /// <response code="200"></response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("")]
    [ActionName("ListerLesProduits")]
    [ProducesResponseType(typeof(List<ProduitResponse>), 200)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult ListerLesProduits()
    {
        var response = _produitService.ListerLesProduits();
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Trouver un produit par son identifiant
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="404">produit_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("{id:int}")]
    [ActionName("TrouverUnProduitParId")]
    [ProducesResponseType(typeof(ProduitResponse), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult TrouverUnProduit(int id)
    {
        var response = _produitService.TrouverUnProduit(id);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Modifier un produit
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produitUpdate"></param>
    /// <returns></returns>
    /// <response code="200">produit_modifie</response>
    /// <response code="400">donnees_invalides</response>
    /// <response code="404">produit_introuvable</response>
    /// <response code="409">produit_existe_deja</response>
    /// <response code="500">unexpected_error</response>
    [HttpPut("{id:int}")]
    [ActionName("ModifierUnProduit")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult ModifierUnProduit(int id, [FromBody] ProduitUpdate produitUpdate)
    {
        var response = _produitService.ModifierUnProduit(id, produitUpdate);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Supprimer un produit
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">produit_supprime</response>
    /// <response code="400">donnees_invalides</response>
    /// <response code="404">produit_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpDelete("{id:int}")]
    [ActionName("SupprimerUnProduit")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult SupprimerUnProduit(int id)
    {
        var response = _produitService.SupprimerUnProduit(id);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Appliquer une promotion sur un produit
    /// </summary>
    /// <param name="produitId"></param>
    /// <param name="promotionId"></param>
    /// <returns></returns>
    /// <response code="200">promotion_applique</response>
    /// <response code="400">donnees_invalides</response>
    /// <response code="404">produit_introuvable | promotion_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpPut("{produitId:int}/promotion/{promotionId:int}")]
    [ActionName("AppliquerUnePromotionSurUnProduit")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult AppliquerUnePromotionSurUnProduit(int produitId, int promotionId)
    {
        var response = _produitService.AppliquerUnePromotion(produitId, promotionId);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Supprimer une promotion d'un produit
    /// </summary>
    /// <param name="produitId"></param>
    /// <returns></returns>
    /// <response code="200">promotion_retiree</response>
    /// <response code="400">donnees_invalides</response>
    /// <response code="404">produit_introuvable | promotion_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpPut("{produitId:int}/promotion")]
    [ActionName("SupprimerUnePromotionSurUnProduit")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult RetirerUnePromotionSurUnProduit(int produitId)
    {
        var response = _produitService.RetirerUnePromotion(produitId);
        return StatusCode(response.StatusCode, response.Data);
    }
}