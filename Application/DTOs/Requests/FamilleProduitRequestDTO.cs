using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests;

public class FamilleProduitRequestDTO
{
    [Required] public required string Nom { get; set; }

    [Required] public required string Description { get; set; }
}