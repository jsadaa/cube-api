using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.Client;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers;

[Route("api/clients")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    /// <summary>
    ///     Ajouter un client
    /// </summary>
    /// <param name="clientRequest"></param>
    /// <returns></returns>
    /// <response code="201">client_ajoute</response>
    /// <response code="400">format_mot_de_passe_invalide</response>
    /// <response code="409">client_existe_deja | utilisateur_existe_deja</response>
    /// <response code="500">unexpected_error</response>
    [HttpPost("")]
    [ActionName("AjouterUnClient")]
    [ProducesResponseType(typeof(ClientResponse), 201)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public async Task<IActionResult> AjouterUnClient([FromBody] ClientRequest clientRequest)
    {
        var response = await _clientService.AjouterUnClient(clientRequest);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Lister les clients
    /// </summary>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("")]
    [ActionName("ListerLesClients")]
    [ProducesResponseType(typeof(IEnumerable<ClientResponse>), 200)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public IActionResult ListerLesClients()
    {
        var response = _clientService.ListerLesClients();
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Trouver un client par son identifiant
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="404">client_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpGet("{id:int}")]
    [ActionName("TrouverUnClientParId")]
    [ProducesResponseType(typeof(ClientResponse), 200)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [Produces("application/json")]
    public IActionResult TrouverUnClientParId(int id)
    {
        var response = _clientService.TrouverUnClient(id);
        return StatusCode(response.StatusCode, response.Data);
    }

    /// <summary>
    ///     Modifier un client
    /// </summary>
    /// <param name="id"></param>
    /// <param name="clientUpdate"></param>
    /// <returns></returns>
    /// <response code="200">client_modifie</response>
    /// <response code="400">format_mot_de_passe_invalide</response>
    /// <response code="404">client_introuvable | utilisateur_introuvable</response>
    /// <response code="409">client_existe_deja | utilisateur_existe_deja</response>
    /// <response code="500">unexpected_error</response>
    [HttpPut("{id:int}")]
    [ActionName("ModifierUnClient")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public async Task<IActionResult> ModifierUnClient(int id, [FromBody] ClientUpdate clientUpdate)
    {
        var response = await _clientService.ModifierUnClient(id, clientUpdate);
        return StatusCode(response.StatusCode, response.Data);
    }


    /// <summary>
    ///     Supprimer un client
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">client_supprime</response>
    /// <response code="404">client_introuvable | utilisateur_introuvable</response>
    /// <response code="500">unexpected_error</response>
    [HttpDelete("{id:int}")]
    [ActionName("SupprimerUnClient")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public async Task<IActionResult> SupprimerUnClient(int id)
    {
        var response = await _clientService.SupprimerUnClient(id);
        return StatusCode(response.StatusCode, response.Data);
    }
}