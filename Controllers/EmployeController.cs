using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
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
        /// <response code="201">employe_ajoute</response>
        /// <response code="400">format_mot_de_passe_invalide</response>
        /// <response code="409">utilisateur_existe_deja | employe_existe_deja</response>
        /// <response code="500">unexpected_error</response>
        [HttpPost("")]
        [ActionName("AjouterUnEmploye")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public async Task<IActionResult> AjouterUnEmploye([FromBody] EmployeRequest employeRequest)
        {
            var response = await _employeService.AjouterUnEmploye(employeRequest);
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Lister les employés
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="500">unexpected_error</response>
        [HttpGet("")]
        [ActionName("ListerLesEmployes")]
        [ProducesResponseType(typeof(IEnumerable<EmployeResponse>), 200)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult ListerLesEmployes()
        {
            var response = _employeService.ListerLesEmployes();
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Trouver un employé par son identifiant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">employe_introuvable</response>
        /// <response code="500">unexpected_error</response>
        [HttpGet("{id:int}")]
        [ActionName("TrouverUnEmployeParSonIdentifiant")]
        [ProducesResponseType(typeof(EmployeResponse), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public IActionResult TrouverUnEmployeParSonIdentifiant(int id)
        {
            var response = _employeService.TrouverUnEmploye(id);
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Modifier un employé
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeRequest"></param>
        /// <returns></returns>
        /// <response code="200">employe_modifie</response>
        /// <response code="400">format_mot_de_passe_invalide | donnees_invalides</response>
        /// <response code="404">employe_introuvable</response>
        /// <response code="409">utilisateur_existe_deja | employe_existe_deja</response>
        /// <response code="500">unexpected_error</response>
        [HttpPut("{id:int}")]
        [ActionName("ModifierUnEmploye")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 400)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 409)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public async Task<IActionResult> ModifierUnEmploye(int id, [FromBody] EmployeRequest employeRequest)
        {
            var response = await _employeService.ModifierUnEmploye(id, employeRequest);
            return StatusCode(response.StatusCode, response.Data);
        }

        /// <summary>
        /// Supprimer un employé
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">employe_supprime</response>
        /// <response code="404">employe_introuvable</response>
        /// <response code="500">unexpected_error</response>
        [HttpDelete("{id:int}")]
        [ActionName("SupprimerUnEmploye")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ExpectedErrorResponse), 404)]
        [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
        [Produces("application/json")]
        public async Task<IActionResult> SupprimerUnEmploye(int id)
        {
            var response = await _employeService.SupprimerUnEmploye(id);
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}