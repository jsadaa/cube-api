using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

[Table("stock")]
public class StockModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Column("quantite")] [Required] public required int Quantite { get; set; }

    [Column("seuil_disponibilite")]
    [Required]
    public required int SeuilDisponibilite { get; set; }

    [Column("statut")]
    [Required]
    [StringLength(50)]
    public required string Statut { get; set; }

    [Column("produit_id")] [Required] public required int ProduitId { get; set; }

    [ForeignKey("ProduitId")] public required ProduitModel Produit { get; set; }

    public ICollection<TransactionStockModel>? TransactionsStock { get; set; }

    [Column("date_creation")] [Required] public required DateTime DateCreation { get; set; }

    [Column("date_peremption")] [Required] public required DateTime DatePeremption { get; set; }

    [Column("date_modification")]
    [Required]
    public required DateTime DateModification { get; set; }

    [Column("date_suppression")] public DateTime? DateSuppression { get; set; }
}