using ApiCube.Application.DTOs;
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
    }
}