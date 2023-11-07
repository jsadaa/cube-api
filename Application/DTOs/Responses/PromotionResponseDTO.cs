using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Responses;

public class PromotionResponseDTO
{
    [Required] public int Id { get; set; }

    [Required] public string Nom { get; set; } = null!;

    [Required] public string Description { get; set; } = null!;

    [Required] public DateTime DateDebut { get; set; }

    [Required] public DateTime DateFin { get; set; }

    [Required] public double Pourcentage { get; set; }
}