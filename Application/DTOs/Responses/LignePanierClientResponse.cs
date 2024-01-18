using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Responses;

public class LignePanierClientResponse
{
    [Required] public int Id { get; set; }
    [Required] public ProduitResponse Produit { get; set; }
    [Required] public int Quantite { get; set; }
    [Required] public double PrixUnitaire { get; set; }
    [Required] public double Total { get; set; }
}