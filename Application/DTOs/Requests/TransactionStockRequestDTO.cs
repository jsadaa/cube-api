using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs;

public class TransactionStockRequestDTO
{
    [Required] public int Quantite { get; set; }

    [Required] public DateTime Date { get; set; }

    [Required] public string Type { get; set; }

    [Required] public int StockId { get; set; }

    [Required] public double PrixUnitaire { get; set; }

    [Required] public double PrixTotal { get; set; }

    [Required] public int QuantiteAvant { get; set; }

    [Required] public int QuantiteApres { get; set; }
}