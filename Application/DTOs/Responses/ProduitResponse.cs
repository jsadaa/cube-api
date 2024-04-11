using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Responses;

public class ProduitResponse
{
    [Required] public int Id { get; set; }

    [Required] public required string Nom { get; set; }

    [Required] public required string Description { get; set; }

    [Required] public required string Appellation { get; set; }

    [Required] public required string Cepage { get; set; }

    [Required] public required string Region { get; set; }

    [Required] public required int Annee { get; set; }

    [Required] public required double DegreAlcool { get; set; }

    [Required] public required double PrixAchat { get; set; }

    [Required] public required double PrixVente { get; set; }

    [Required] public required DateTime DatePeremption { get; set; }

    [Required] public required bool EnPromotion { get; set; }
    
    public PromotionResponse? Promotion { get; set; }

    [Required] public required string FamilleProduitNom { get; set; } = null!;

    [Required] public required string FournisseurNom { get; set; } = null!;
}