using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests;

public class StockRequestDTO
{
    [Required] public int Quantite { get; set; }

    [Required] public int SeuilDisponibilite { get; set; }

    [Required] public int ProduitId { get; set; }

    [Required] public DateTime DatePeremption { get; set; }
}