using Microsoft.Build.Framework;

namespace ApiCube.DTOs;

public class AjouterFamilleProduitRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Nom { get; set; } = null!;
    
    [Required]
    public string Description { get; set; } = null!;
}