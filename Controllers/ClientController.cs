using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.Client;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers
{
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
        /// Ajouter un client
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
        /// <response code="201">client_ajoute</response>
        /// <response code="400">format_mot_de_passe_invalide</response>
        /// <response code="409">client_existe_deja | utilisateur_existe_deja</response>
        /// <response code="500">unexpected_error</response>
        [HttpPost("")]
        [ActionName("AjouterUnClient")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public async Task<IActionResult> AjouterUnClient([FromBody] ClientRequest clientRequest)
        {
            var response = await _clientService.AjouterUnClient(clientRequest);
            return StatusCode(response.StatusCode, response.Data);
        }
        /*
        /// <summary>
        /// Lister les clients
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="500">unexpected_error</response>
        [HttpGet("")]
        [ActionName("ListerLesClients")]
        [ProducesResponseType(typeof(IEnumerable<ClientResponse>), 200)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public async Task<IActionResult> ListerLesClients()
        {
            var response = await _clientService.ListerLesClients();
            return StatusCode(response.StatusCode, response.Data);
        }
        */
    }
}