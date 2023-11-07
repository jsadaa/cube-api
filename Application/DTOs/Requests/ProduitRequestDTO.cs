using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests;

public class ProduitRequestDTO
{
    [Required] public string Nom { get; set; }

    [Required] public string Description { get; set; }

    [Required] public string Appellation { get; set; }

    [Required] public string Cepage { get; set; }

    [Required] public string Region { get; set; }

    [Required] public double DegreAlcool { get; set; }

    [Required] public double PrixAchat { get; set; }

    [Required] public double PrixVente { get; set; }

    [Required] public bool EnPromotion { get; set; }

    [Required] public int FamilleProduitId { get; set; }

    [Required] public int FournisseurId { get; set; }
}