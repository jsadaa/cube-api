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
    
    [Column("quantite")]
    [Required]
    public int Quantite { get; set; }
    
    [Column("date")]
    [Required]
    public DateTime Date { get; set; }
    
    [Column("type")]
    [Required]
    [StringLength(50)]
    public string Type { get; set; }
    
    [Column("stock_id")]
    [Required]
    public int StockId { get; set; }
    
    [ForeignKey("StockId")]
    public StockModel Stock { get; set; }
    
    [Column("prix_unitaire")]
    [Required]
    public double PrixUnitaire { get; set; }
    
    [Column("prix_total")]
    [Required]
    public double PrixTotal { get; set; }
    
    [Column("quantite_avant")]
    [Required]
    public int QuantiteAvant { get; set; }
    
    [Column("quantite_apres")]
    [Required]
    public int QuantiteApres { get; set; }
}