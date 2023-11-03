using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;
using ApiCube.Services.Produit;
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
    public IActionResult AjouterUnProduitAuStock([FromBody] AjouterProduitRequest ajouterProduit)
    {
        BaseResponse response = _produitService.AjouterUnProduitAuStock(ajouterProduit);
        
        return StatusCode(response.StatusCode, response.Data);
    }
    
    [HttpGet("lister")]
    [ProducesResponseType(typeof(List<ProduitDTO>), 200)]
    public IActionResult ListerLesProduits()
    {
        BaseResponse response = _produitService.ListerLesProduits();
        
        return StatusCode(response.StatusCode, response.Data);
    }
}