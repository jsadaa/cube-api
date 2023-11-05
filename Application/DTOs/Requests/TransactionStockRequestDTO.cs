using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests;

public class TransactionStockRequestDTO
{
    [Required]
    public int Quantite { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public string Type { get; set; }
    
    [Required]
    public int ProduitId { get; set; } 
    
    [Required]
    public int StockId { get; set; }
}