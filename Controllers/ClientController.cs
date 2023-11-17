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
        /// <response code="400">Le client n'a pas pu être ajouté</response>
        /// <response code="409">Le client existe déjà</response>
        /// <response code="500">Erreur interne</response>
        [HttpPost("")]
        [ActionName("AjouterUnClient")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public IActionResult AjouterUnClient([FromBody] ClientRequest clientRequest)
        {
            var task = _clientService.AjouterUnClient(clientRequest);
            var response = task.Result;

            return StatusCode(response.StatusCode, response.Data);
        }
    }
}