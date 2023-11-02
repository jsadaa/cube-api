using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;
using ApiCube.Services.ProduitService;
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
    public IActionResult AjouterUnProduitAuStock([FromBody] AjouterProduitRequest ajouterProduit)
    {
        BaseResponse response = _produitService.AjouterUnProduitAuStock(ajouterProduit);
        
        return StatusCode(response.StatusCode, response.Data);
    }
    
    [HttpGet("lister")]
    public IActionResult ListerLesProduits()
    {
        BaseResponse response = _produitService.ListerLesProduits();
        
        return StatusCode(response.StatusCode, response.Data);
    }
}