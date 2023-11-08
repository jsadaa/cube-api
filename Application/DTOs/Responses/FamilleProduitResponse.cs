using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Responses;

public class FamilleProduitResponse
{
    [Required] public required int Id { get; set; }

    [Required] public required string Nom { get; set; }

    [Required] public required string Description { get; set; }
}