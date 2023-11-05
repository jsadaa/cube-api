using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests;

public class StockRequestDTO
{
    
    [Required]
    public int Quantite { get; set; }
    
    [Required]
    public int SeuilDisponibilite { get; set; }
    
    public string Statut { get; set; }
    
    [Required]
    public int ProduitId { get; set; }
    
    public DateTime DateCreation { get; set; }
    
    [Required]
    public DateTime DatePeremption { get; set; }
    
    public DateTime DateModification { get; set; }
    
    public DateTime? DateSuppression { get; set; }
}