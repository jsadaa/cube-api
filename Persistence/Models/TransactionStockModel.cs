using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

[Table("transaction_stock")]
public class TransactionStockModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Column("variation_quantite")]
    [Required]
    public required int VariationQuantite { get; set; }

    [Column("date")] [Required] public required DateTime Date { get; set; }

    [Column("type")]
    [Required]
    [StringLength(50)]
    public required string Type { get; set; }

    [Column("stock_id")] [Required] public required int StockId { get; set; }

    [ForeignKey("StockId")] public required StockModel Stock { get; set; }

    [Column("prix_unitaire")] [Required] public required double PrixUnitaire { get; set; }

    [Column("prix_total")] [Required] public required double PrixTotal { get; set; }

    [Column("quantite_avant")] [Required] public required int QuantiteAvant { get; set; }

    [Column("quantite_apres")] [Required] public required int QuantiteApres { get; set; }
}