using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests.Stock;

public class StockUpdate
{
    [Required] public required int Quantite { get; set; }

    [Required] public required int SeuilDisponibilite { get; set; }
    
    [Required] public required DateTime DatePeremption { get; set; }
}