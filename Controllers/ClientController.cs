using ApiCube.Application.DTOs.Requests;
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
        /// <response code="201">Le client a été ajouté avec succès</response>
        /// <response code="400">Données invalides</response>
        /// <response code="409">Le client existe déjà</response>
        /// <response code="500">Erreur interne</response>
        [HttpPost("")]
        [ActionName("AjouterUnClient")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public async Task<IActionResult> AjouterUnClient([FromBody] ClientRequest clientRequest)
        {
            var response = await _clientService.AjouterUnClient(clientRequest);
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}