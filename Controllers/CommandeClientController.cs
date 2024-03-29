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
    ///     Trouver un panier par l'id du client
    /// </summary>
    /// <param name="idClient"></param>
    /// <returns></returns>
    /// <response code="200">panier_trouve</response>
    /// <response code="404">panier_introuvable | client_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("panier/client/{idClient:int}")]
    [ActionName("TrouverUnPanierParIdClient")]
    [ProducesResponseType(typeof(PanierClientResponse), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult TrouverUnPanierParIdClient(int idClient)
    {
        var response = _commandeClientService.TrouverUnPanierParClient(idClient);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Supprimer un produit du panier
    /// </summary>
    /// <param name="idPanier"></param>
    /// <param name="idProduit"></param>
    /// <returns></returns>
    /// <response code="200">produit_supprime_du_panier</response>
    /// <response code="404">produit_introuvable | panier_client_introuvable</response>
    /// <response code="409">produit_non_present_dans_panier</response>
    /// <response code="500">unexpected_error</response>
    [HttpDelete("panier/{idPanier:int}/produit/{idProduit:int}")]
    [ActionName("SupprimerUnProduitDuPanier")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult SupprimerUnProduitDuPanier(int idPanier, int idProduit)
    {
        var response = _commandeClientService.SupprimerUnProduitDuPanier(idPanier, idProduit);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Modifier la quantité d'un produit dans le panier
    /// </summary>
    /// <param name="idPanier"></param>
    /// <param name="produitPanierUpdate"></param>
    /// <returns></returns>
    /// <response code="200">quantite_produit_modifiee</response>
    /// <response code="400">quantite_panier_invalide</response>
    /// <response code="404">produit_introuvable | panier_client_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpPut("panier/{idPanier:int}/produit")]
    [ActionName("ModifierLaQuantiteDUnProduitDansLePanier")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult ModifierLaQuantiteDUnProduitDansLePanier(int idPanier,
        [FromBody] ProduitPanierUpdate produitPanierUpdate)
    {
        var response = _commandeClientService.ModifierLaQuantiteDUnProduitDansLePanier(idPanier, produitPanierUpdate);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Vider un panier
    /// </summary>
    /// <param name="idPanier"></param>
    /// <returns></returns>
    /// <response code="200">panier_vide</response>
    /// <response code="404">panier_client_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpDelete("panier/{idPanier:int}/vider")]
    [ActionName("ViderUnPanier")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult ViderUnPanier(int idPanier)
    {
        var response = _commandeClientService.ViderUnPanier(idPanier);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Supprimer le panier d'un client
    /// </summary>
    /// <param name="idClient"></param>
    /// <returns></returns>
    /// <response code="200">panier_supprime</response>
    /// <response code="404">client_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpDelete("panier/{idClient:int}")]
    [ActionName("SupprimerUnPanier")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult SupprimerUnPanier(int idClient)
    {
        var response = _commandeClientService.SupprimerUnPanier(idClient);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Valider un panier (passer la commande)
    /// </summary>
    /// <param name="idPanier"></param>
    /// <returns></returns>
    /// <response code="200">panier_valide</response>
    /// <response code="404">panier_client_introuvable | client_introuvable | stock_introuvable | produit_introuvable</response>
    /// <response code="409">quantite_stock_insuffisant</response>
    /// <response code="500">unexpected_error</response>
    [HttpPut("panier/{idPanier:int}/valider")]
    [ActionName("ValiderUnPanier")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult ValiderUnPanier(int idPanier)
    {
        var response = _commandeClientService.ValiderUnPanier(idPanier);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     trouver une commande par son id
    /// </summary>
    /// <param name="idCommande"></param>
    /// <returns></returns>
    /// <response code="200">commande_trouvee</response>
    /// <response code="404">commande_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("commande/{idCommande:int}")]
    [ActionName("TrouverUneCommandeParId")]
    [ProducesResponseType(typeof(CommandeClientResponse), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult TrouverUneCommandeParId(int idCommande)
    {
        var response = _commandeClientService.TrouverUneCommande(idCommande);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Lister les commandes d'un client
    /// </summary>
    /// <param name="idClient"></param>
    /// <returns></returns>
    /// <response code="200">commandes_trouvees</response>
    /// <response code="404">client_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("commande/client/{idClient:int}")]
    [ActionName("ListerLesCommandesDUnClient")]
    [ProducesResponseType(typeof(CommandeClientResponse), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult ListerLesCommandesDUnClient(int idClient)
    {
        var response = _commandeClientService.ListerLesCommandesDUnClient(idClient);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Lister les commandes
    /// </summary>
    /// <returns></returns>
    /// <response code="200">commandes_trouvees</response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("commande")]
    [ActionName("ListerLesCommandes")]
    [ProducesResponseType(typeof(CommandeClientResponse), 200)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult ListerLesCommandes()
    {
        var response = _commandeClientService.ListerLesCommandes();
        return StatusCode(response.StatusCode, response.Data);
    }
}
