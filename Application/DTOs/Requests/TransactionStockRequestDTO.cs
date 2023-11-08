using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests;

public class TransactionStockRequestDTO
{
    [Required] public required int Quantite { get; set; }

    [Required] public required DateTime Date { get; set; }

    [Required] public required string Type { get; set; }

    [Required] public required int StockId { get; set; }
}