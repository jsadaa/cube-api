using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Responses;

public class PromotionResponseDTO
{
    [Required] public required int Id { get; set; }

    [Required] public required string Nom { get; set; } = null!;

    [Required] public required string Description { get; set; } = null!;

    [Required] public required DateTime DateDebut { get; set; }

    [Required] public required DateTime DateFin { get; set; }

    [Required] public required double Pourcentage { get; set; }
}