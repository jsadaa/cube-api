using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Responses;

public class FamilleProduitResponseDTO
{
    [Required] public int Id { get; set; }

    [Required] public string Nom { get; set; } = null!;

    [Required] public string Description { get; set; } = null!;
}