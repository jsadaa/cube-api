using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Models;

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
    
    [Column("produit_id")]
    [Required]
    public int ProduitId { get; set; }
    
    [ForeignKey("ProduitId")]
    public ProduitModel Produit { get; set; }
    
    [Column("employe_id")]
    [Required]
    public int EmployeId { get; set; }
    
    [ForeignKey("EmployeId")]
    public EmployeModel Employe { get; set; }
    
    // prix unitaire
    [Column("prix_unitaire")]
    [Required]
    public double PrixUnitaire { get; set; }
    
    // prix total
    [Column("prix_total")]
    [Required]
    public double PrixTotal { get; set; }
}