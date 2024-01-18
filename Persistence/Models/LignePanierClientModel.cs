using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

[Table("ligne_panier_client")]
public class LignePanierClientModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Column("quantite")] [Required] public required int Quantite { get; set; }

    [Column("prix_unitaire")] [Required] public required double PrixUnitaire { get; set; }

    [Column("remise")] [Required] public required double Remise { get; set; }

    [Column("total")] [Required] public required double Total { get; set; }

    [Column("produit_id")] [Required] public required int ProduitId { get; set; }

    [Column("panier_client_id")]
    [Required]
    public int PanierClientId { get; set; }

    [ForeignKey("ProduitId")] public required ProduitModel Produit { get; set; }

    [ForeignKey("PanierClientId")] public required PanierClientModel PanierClient { get; set; }
}