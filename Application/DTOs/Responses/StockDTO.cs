using System.ComponentModel.DataAnnotations;
using ApiCube.Domain.Entities;

namespace ApiCube.Application.DTOs.Responses;

public class StockDTO
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public int Quantite { get; set; }
    
    [Required]
    public int SeuilDisponibilite { get; set; }
    
    [Required]
    public string Statut { get; set; }
    
    [Required]
    public ProduitDTO Produit { get; set; }
    
    [Required]
    public DateTime DateCreation { get; set; }
    
    [Required]
    public DateTime DatePeremption { get; set; }
    
    [Required]
    public DateTime DateModification { get; set; }
    
    public DateTime? DateSuppression { get; set; }
}