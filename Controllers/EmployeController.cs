using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.Services.Employe;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers
{
    [Route("api/employes")]
    [ApiController]
    public class EmployeController : ControllerBase
    {
        private readonly IEmployeService _employeService;

        public EmployeController(IEmployeService employeService)
        {
            _employeService = employeService;
        }

        /// <summary>
        /// Ajouter un employé
        /// </summary>
        /// <param name="employeRequest"></param>
        /// <returns></returns>
        /// <response code="201">L'employé a été ajouté avec succès</response>
        /// <response code="400">Données invalides</response>
        /// <response code="409">L'employé existe déjà</response>
        /// <response code="500">Erreur interne</response>
        [HttpPost("")]
        [ActionName("AjouterUnEmploye")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 500)]
        [Produces("application/json")]
        public async Task<IActionResult> AjouterUnEmploye([FromBody] EmployeRequest employeRequest)
        {
            var response = await _employeService.AjouterUnEmploye(employeRequest);
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}