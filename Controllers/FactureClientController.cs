using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.FactureClient;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers;

[Route("api/factures")]
[ApiController]
public class FactureClientController : ControllerBase
{
    private readonly IFactureClientService _factureService;

    public FactureClientController(IFactureClientService factureService)
    {
        _factureService = factureService;
    }

    /// <summary>
    ///     Trouver une facture par son identifiant
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="404">facture_non_trouvee</response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("{id:int}")]
    [ActionName("TrouverUneFactureParSonIdentifiant")]
    [ProducesResponseType(typeof(FactureClientResponse), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult TrouverUneFactureParSonIdentifiant([FromRoute] int id)
    {
        var response = _factureService.TrouverUneFacture(id);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Lister les factures
    /// </summary>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("")]
    [ActionName("ListerLesFactures")]
    [ProducesResponseType(typeof(IEnumerable<FactureClientResponse>), 200)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult ListerLesFactures()
    {
        var response = _factureService.ListerLesFactures();
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Marquer une facture comme payée
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">facture_marquee_comme_payee</response>
    /// <response code="404">facture_non_trouvee</response>
    /// <response code="500">unexpected_error</response>
    [HttpPut("{id:int}/payer")]
    [ActionName("MarquerUneFactureCommePayee")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult MarquerUneFactureCommePayee([FromRoute] int id)
    {
        var response = _factureService.MarquerUneFactureCommePayee(id);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Lister les factures d'un client
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="404">client_non_trouve</response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("clients/{id:int}")]
    [ActionName("ListerLesFacturesDUnClient")]
    [ProducesResponseType(typeof(IEnumerable<FactureClientResponse>), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult ListerLesFacturesDUnClient([FromRoute] int id)
    {
        var response = _factureService.ListerLesFacturesDUnClient(id);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Trouver une facture par commande
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="404">facture_non_trouvee</response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("commandes/{id:int}")]
    [ActionName("TrouverUneFactureParCommande")]
    [ProducesResponseType(typeof(FactureClientResponse), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult TrouverUneFactureParCommande([FromRoute] int id)
    {
        var response = _factureService.TrouverUneFactureParCommande(id);
        return StatusCode(response.StatusCode, response.Data);
    }
}