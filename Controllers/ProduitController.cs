using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.Produit;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers;

[Route("api/produits")]
[ApiController]
public class ProduitController : ControllerBase
{
    private readonly IProduitService _produitService;
    
    public ProduitController(IProduitService produitService)
    {
        _produitService = produitService;
    }
    
    [HttpPost("ajouter")]
    [ProducesResponseType(typeof(string), 201)]
    [ProducesResponseType(typeof(string), 400)]
    public IActionResult AjouterUnProduitAuCatalogue([FromBody] ProduitRequestDTO produitRequestDTO)
    {
        BaseResponse response = _produitService.AjouterUnProduitAuCatalogue(produitRequestDTO);
        
        return StatusCode(response.StatusCode, response.Data);
    }
    
    [HttpGet("lister")]
    [ProducesResponseType(typeof(List<ProduitResponseDTO>), 200)]
    public IActionResult ListerLesProduits()
    {
        BaseResponse response = _produitService.ListerLesProduits();
        
        return StatusCode(response.StatusCode, response.Data);
    }
}