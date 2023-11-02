using System.ComponentModel.DataAnnotations;

namespace ApiCube.DTOs.Requests;

public class AjouterProduitRequest
{
    
    [Required]
    public string Nom { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public int Quantite { get; set; }
    
    [Required]
    public int SeuilDisponibilite { get; set; }
    
    [Required]
    public string StatutStock { get; set; }
    
    [Required]
    public double PrixAchat { get; set; }
    
    [Required]
    public double PrixVente { get; set; }
    
    [Required]
    public DateTime DateAchat { get; set; }
    
    [Required]
    public DateTime DatePeremption { get; set; }
    
    [Required]
    public int FamilleProduitId { get; set; }
}