using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests.Produit;

public class ProduitRequest
{
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

    [Required] public required int FamilleProduitId { get; set; }

    [Required] public required int FournisseurId { get; set; }
}