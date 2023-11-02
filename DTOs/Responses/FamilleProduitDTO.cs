using Microsoft.Build.Framework;

namespace ApiCube.DTOs.Responses;

public class FamilleProduitDTO
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Nom { get; set; } = null!;
    
    [Required]
    public string Description { get; set; } = null!;
}