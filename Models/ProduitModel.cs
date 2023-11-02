using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Models;

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
    
    [Column("quantite")]
    [Required]
    public int Quantite { get; set; }
    
    [Column("seuil_disponibilite")]
    [Required]
    public int SeuilDisponibilite { get; set; }
    
    [Column("statut_stock")]
    [Required]
    [StringLength(50)]
    public string StatutStock { get; set; }
    
    [Column("prix_achat")]
    [Required]
    public double PrixAchat { get; set; }
    
    [Column("prix_vente")]
    [Required]
    public double PrixVente { get; set; }
    
    [Column("date_achat")]
    [Required]
    public DateTime DateAchat { get; set; }
    
    [Column("date_peremption")]
    [Required]
    public DateTime DatePeremption { get; set; }
    
    // a des Promotions
    public ICollection<PromotionModel> Promotions { get; set; }
    
    // a une FamilleProduit
    [Column("famille_produit_id")]
    [Required]
    public int FamilleProduitId { get; set; }
    
    [ForeignKey("FamilleProduitId")]
    public FamilleProduitModel FamilleProduit { get; set; }
}