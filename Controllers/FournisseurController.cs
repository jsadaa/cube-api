using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;
using ApiCube.Services.Fournisseur;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers
{
    [Route("api/fournisseurs")]
    [ApiController]
    public class FournisseurController : ControllerBase
    {
        private readonly IFournisseurService _fournisseurService;
        
        public FournisseurController(IFournisseurService fournisseurService)
        {
            _fournisseurService = fournisseurService;
        }
        
        [HttpPost("ajouter")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult AjouterUnFournisseur([FromBody] AjouterFournisseurRequest ajouterFournisseurRequest)
        {
            BaseResponse response = _fournisseurService.AjouterUnFournisseur(ajouterFournisseurRequest);
            
            return StatusCode(response.StatusCode, response.Data);
        }
        
        [HttpGet("lister")]
        [ProducesResponseType(typeof(List<FournisseurDTO>), 200)]
        public IActionResult ListerLesFournisseurs()
        {
            BaseResponse response = _fournisseurService.ListerLesFournisseurs();
            
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
