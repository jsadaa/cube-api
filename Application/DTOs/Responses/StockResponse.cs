using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Responses;

public class StockResponse
{
    [Required] public required int Id { get; set; }

    [Required] public required int Quantite { get; set; }

    [Required] public required int SeuilDisponibilite { get; set; }

    [Required] public required string Statut { get; set; }

    [Required] public required ProduitResponse Produit { get; set; }

    [Required] public required DateTime DateCreation { get; set; }

    [Required] public required DateTime DatePeremption { get; set; }

    [Required] public required DateTime DateModification { get; set; }

    public DateTime? DateSuppression { get; set; }
}