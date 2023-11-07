using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Responses;

public class ProduitResponseDTO
{
    [Required] public int Id { get; set; }

    [Required] public string Nom { get; set; }

    [Required] public string Description { get; set; }

    [Required] public string Appellation { get; set; }

    [Required] public string Cepage { get; set; }

    [Required] public string Region { get; set; }

    [Required] public double DegreAlcool { get; set; }

    [Required] public double PrixAchat { get; set; }

    [Required] public double PrixVente { get; set; }

    [Required] public bool EnPromotion { get; set; }

    [Required] public string FamilleProduitNom { get; set; } = null!;

    [Required] public string FournisseurNom { get; set; } = null!;
}