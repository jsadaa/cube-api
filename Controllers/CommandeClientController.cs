using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.Services.CommandeClient;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers;

[Route("api/commandes-clients/panier")]
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
    [HttpPost("{idClient:int}")]
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
    /// <param name="id"></param>
    /// <param name="produitPanierRequest"></param>
    /// <returns></returns>
    /// <response code="201">produit_ajoute_au_panier</response>
    /// <response code="400">quantite_panier_invalide</response>
    /// <response code="404">produit_introuvable | client_introuvable</response>
    /// <response code="409">produit_deja_dans_panier</response>
    /// <response code="500">unexpected_error</response>
    [HttpPost("{id:int}/produit")]
    [ActionName("AjouterUnProduitAuPanier")]
    [ProducesResponseType(typeof(string), 201)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult AjouterUnProduitAuPanier(int id, [FromBody] ProduitPanierRequest produitPanierRequest)
    {
        var response = _commandeClientService.AjouterUnProduitAuPanier(id, produitPanierRequest);
        return StatusCode(response.StatusCode, response.Data);
    }
}