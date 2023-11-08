using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests;

public class PromotionRequestDTO
{
    [Required] public required string Nom { get; set; }

    [Required] public required string Description { get; set; }

    [Required] public required DateTime DateDebut { get; set; }

    [Required] public required DateTime DateFin { get; set; }

    [Required] public required double Pourcentage { get; set; }
}