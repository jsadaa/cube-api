using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.CommandeClient;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers;

[Route("api/commandes-clients")]
[ApiController]
public class CommandeClientController : ControllerBase
{
    private readonly ICommandeClientService _commandeClientService;

    public CommandeClientController(ICommandeClientService commandeClientService)
    {
        _commandeClientService = commandeClientService;
    }

    /// <summary>
    ///     Créer un nouveau panier pour un client
    /// </summary>
    /// <param name="idClient"></param>
    /// <returns></returns>
    /// <response code="201">panier_cree</response>
    /// <response code="404">client_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpPost("panier/{idClient:int}")]
    [ActionName("CreerUnPanier")]
    [ProducesResponseType(typeof(string), 201)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult CreerUnPanier(int idClient)
    {
        var response = _commandeClientService.CreerUnPanier(idClient);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Ajouter un produit au panier
    /// </summary>
    /// <param name="idPanier"></param>
    /// <param name="produitPanierRequest"></param>
    /// <returns></returns>
    /// <response code="201">produit_ajoute_au_panier</response>
    /// <response code="400">quantite_panier_invalide</response>
    /// <response code="404">produit_introuvable | client_introuvable</response>
    /// <response code="409">produit_deja_dans_panier</response>
    /// <response code="500">unexpected_error</response>
    [HttpPost("panier/{idPanier:int}/produit")]
    [ActionName("AjouterUnProduitAuPanier")]
    [ProducesResponseType(typeof(string), 201)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult AjouterUnProduitAuPanier(int idPanier, [FromBody] ProduitPanierRequest produitPanierRequest)
    {
        var response = _commandeClientService.AjouterUnProduitAuPanier(idPanier, produitPanierRequest);
        return StatusCode(response.StatusCode, response.Data);
    }
    
    /// <summary>
    ///     Trouver un panier par son id
    /// </summary>
    /// <param name="idPanier"></param>
    /// <returns></returns>
    /// <response code="200">panier_trouve</response>
    /// <response code="404">panier_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("panier/{idPanier:int}")]
    [ActionName("TrouverUnPanierParId")]
    [ProducesResponseType(typeof(PanierClientResponse), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult TrouverUnPanierParId(int idPanier)
    {
        var response = _commandeClientService.TrouverUnPanier(idPanier);
        return StatusCode(response.StatusCode, response.Data);
    }
    
    /// <summary>
    ///    Lister les paniers d'un client
    /// </summary>
    /// <param name="idClient"></param>
    /// <returns></returns>
    /// <response code="200">paniers_trouves</response>
    /// <response code="404">client_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("panier/client/{idClient:int}")]
    [ActionName("ListerLesPaniersDUnClient")]
    [ProducesResponseType(typeof(PanierClientResponse), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult ListerLesPaniersDUnClient(int idClient)
    {
        var response = _commandeClientService.ListerLesPaniersDUnClient(idClient);
        return StatusCode(response.StatusCode, response.Data);
    }
    
}