using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

[Table("produit")]
public class ProduitModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Column("nom")]
    [Required]
    [StringLength(50)]
    public required string Nom { get; set; }

    [Column("description")]
    [Required]
    [StringLength(200)]
    public required string Description { get; set; }

    [Column("appellation")]
    [Required]
    [StringLength(50)]
    public required string Appellation { get; set; }

    [Column("cepage")]
    [Required]
    [StringLength(50)]
    public required string Cepage { get; set; }

    [Column("region")]
    [Required]
    [StringLength(50)]
    public required string Region { get; set; }

    [Column("annee")] [Required] public required int Annee { get; set; }

    [Column("degre_alcool")] [Required] public required double DegreAlcool { get; set; }

    [Column("prix_achat")] [Required] public required double PrixAchat { get; set; }

    [Column("prix_vente")] [Required] public required double PrixVente { get; set; }
    
    [Column("date_peremption")] [Required] public required DateTime DatePeremption { get; set; }

    [Column("en_promotion")] [Required] public required bool EnPromotion { get; set; }

    [Column("promotion_id")] public int? PromotionId { get; set; }

    [ForeignKey("PromotionId")] public PromotionModel? Promotion { get; set; }

    [Column("famille_produit_id")]
    [Required]
    public required int FamilleProduitId { get; set; }

    [ForeignKey("FamilleProduitId")] public required FamilleProduitModel FamilleProduit { get; set; }

    [Column("fournisseur_id")] [Required] public required int FournisseurId { get; set; }

    [ForeignKey("FournisseurId")] public required FournisseurModel Fournisseur { get; set; }

    public ICollection<StockModel>? Stocks { get; set; }
}