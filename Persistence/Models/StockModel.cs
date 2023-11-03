using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

public class StockModel
{
    [Column ("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    
    [Column("quantite")]
    [Required]
    public int Quantite { get; set; }
    
    [Column("seuil_disponibilite")]
    [Required]
    public int SeuilDisponibilite { get; set; }
    
    [Column("statut")]
    [Required]
    [StringLength(50)]
    public string Statut { get; set; }
    
    [Column("produit_id")]
    [Required]
    public int ProduitId { get; set; }
    
    [ForeignKey("ProduitId")]
    public ProduitModel Produit { get; set; }
    
    public ICollection<TransactionStockModel> TransactionsStock { get; set; }
    
    [Column("date_creation")]
    [Required]
    public DateTime DateCreation { get; set; }
    
    [Column("date_peremption")]
    [Required]
    public DateTime DatePeremption { get; set; }
    
    [Column("date_modification")]
    [Required]
    public DateTime DateModification { get; set; }
    
    [Column("date_suppression")]
    public DateTime? DateSuppression { get; set; }
    
    [Column("est_supprime")]
    [Required]
    public bool EstSupprime { get; set; }
}