using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests;

public class StockRequestDTO
{
    [Required] public required int Quantite { get; set; }

    [Required] public required int SeuilDisponibilite { get; set; }

    [Required] public required int ProduitId { get; set; }

    [Required] public required DateTime DatePeremption { get; set; }
}