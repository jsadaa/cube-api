using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Responses;

public class TransactionStockResponseDTO
{
    [Required] public required int Id { get; set; }

    [Required] public required int Quantite { get; set; }

    [Required] public required DateTime Date { get; set; }

    [Required] public required string Type { get; set; }

    [Required] public required int StockId { get; set; }

    [Required] public required double PrixUnitaire { get; set; }

    [Required] public required double PrixTotal { get; set; }

    [Required] public required int QuantiteAvant { get; set; }

    [Required] public required int QuantiteApres { get; set; }
}