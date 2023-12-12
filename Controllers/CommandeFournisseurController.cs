using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.CommandeFournisseur;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers;

[Route("api/commandes-fournisseurs")]
[ApiController]
public class CommandeFournisseurController : ControllerBase
{
    private readonly ICommandeFournisseurService _commandeFournisseurService;

    public CommandeFournisseurController(ICommandeFournisseurService commandeFournisseurService)
    {
        _commandeFournisseurService = commandeFournisseurService;
    }

    /// <summary>
    ///     Ajouter une commande fournisseur
    /// </summary>
    /// <param name="commandeFournisseurRequest"></param>
    /// <returns></returns>
    /// <response code="201">commande_fournisseur_ajoute</response>
    /// <response code="400">statut_commande_invalide</response>
    /// <response code="404">fournisseur_introuvable | employe_introuvable | produit_introuvable</response>
    /// <response code="409">commande_fournisseur_existe_deja</response>
    /// <response code="500">unexpected_error</response>
    [HttpPost("")]
    [ActionName("AjouterUneCommandeFournisseur")]
    [ProducesResponseType(typeof(string), 201)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult AjouterUneCommandeFournisseur(
        [FromBody] CommandeFournisseurRequest commandeFournisseurRequest)
    {
        var response = _commandeFournisseurService.AjouterUneCommandeFournisseur(commandeFournisseurRequest);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Lister les commandes fournisseurs
    /// </summary>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("")]
    [ActionName("ListerLesCommandesFournisseurs")]
    [ProducesResponseType(typeof(IEnumerable<CommandeFournisseurResponse>), 200)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult ListerLesCommandesFournisseurs()
    {
        var response = _commandeFournisseurService.ListerLesCommandesFournisseurs();
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Trouver une commande fournisseur par son identifiant
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="404">commande_fournisseur_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("{id:int}")]
    [ActionName("TrouverUneCommandeFournisseurParId")]
    [ProducesResponseType(typeof(CommandeFournisseurResponse), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult TrouverUneCommandeFournisseurParId(int id)
    {
        var response = _commandeFournisseurService.TrouverUneCommandeFournisseur(id);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Modifier une commande fournisseur
    /// </summary>
    /// <param name="id"></param>
    /// <param name="commandeFournisseurRequest"></param>
    /// <returns></returns>
    /// <response code="200">commande_fournisseur_modifiee</response>
    /// <response code="400">date_commande_invalide | commande_deja_livree | commande_annulee | statut_commande_invalide</response>
    /// <response code="404">commande_fournisseur_introuvable | fournisseur_introuvable | employe_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpPut("{id:int}")]
    [ActionName("ModifierUneCommandeFournisseur")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult ModifierUneCommandeFournisseur(int id,
        [FromBody] CommandeFournisseurRequest commandeFournisseurRequest)
    {
        var response = _commandeFournisseurService.ModifierUneCommandeFournisseur(id, commandeFournisseurRequest);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Supprimer une commande fournisseur
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">commande_fournisseur_supprimee</response>
    /// <response code="400">date_commande_invalide | commande_deja_livree | commande_annulee</response>
    /// <response code="404">commande_fournisseur_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpDelete("{id:int}")]
    [ActionName("SupprimerUneCommandeFournisseur")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult SupprimerUneCommandeFournisseur(int id)
    {
        var response = _commandeFournisseurService.SupprimerUneCommandeFournisseur(id);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Marquer une commande fournisseur comme livrée
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">commande_fournisseur_marquee_comme_livree</response>
    /// <response code="400">date_commande_invalide | commande_deja_livree | commande_annulee</response>
    /// <response code="404">commande_fournisseur_introuvable | stock_introuvable | produit_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpPut("{id:int}/marquer-comme-livree")]
    [ActionName("MarquerUneCommandeFournisseurCommeLivree")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult MarquerUneCommandeFournisseurCommeLivree(int id)
    {
        var response = _commandeFournisseurService.MarquerUneCommandeFournisseurCommeLivree(id);
        return StatusCode(response.StatusCode, response.Data);
    }
}