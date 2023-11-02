
using System.ComponentModel.DataAnnotations;

namespace ApiCube.DTOs.Requests;

public class AjouterFamilleProduitRequest
{
    [Required]
    public string Nom { get; set; } = null!;
    
    [Required]
    public string Description { get; set; } = null!;
}