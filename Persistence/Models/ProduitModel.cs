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
    public string Nom { get; set; }

    [Column("description")]
    [Required]
    [StringLength(200)]
    public string Description { get; set; }

    [Column("appellation")]
    [Required]
    [StringLength(50)]
    public string Appellation { get; set; }

    [Column("cepage")]
    [Required]
    [StringLength(50)]
    public string Cepage { get; set; }

    [Column("region")]
    [Required]
    [StringLength(50)]
    public string Region { get; set; }

    [Column("degre_alcool")] [Required] public double DegreAlcool { get; set; }

    [Column("prix_achat")] [Required] public double PrixAchat { get; set; }

    [Column("prix_vente")] [Required] public double PrixVente { get; set; }

    [Column("en_promotion")] [Required] public bool EnPromotion { get; set; }

    [Column("promotion_id")] public int? PromotionId { get; set; }

    [ForeignKey("PromotionId")] public PromotionModel? Promotion { get; set; }

    [Column("famille_produit_id")]
    [Required]
    public int FamilleProduitId { get; set; }

    [ForeignKey("FamilleProduitId")] public FamilleProduitModel FamilleProduit { get; set; }

    [Column("fournisseur_id")] [Required] public int FournisseurId { get; set; }

    [ForeignKey("FournisseurId")] public FournisseurModel Fournisseur { get; set; }

    public ICollection<StockModel> Stocks { get; set; }
}