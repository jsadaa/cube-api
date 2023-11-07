using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests;

public class FamilleProduitRequestDTO
{
    [Required] public string Nom { get; set; } = null!;

    [Required] public string Description { get; set; } = null!;
}