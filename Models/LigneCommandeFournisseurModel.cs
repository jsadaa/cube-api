using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Models;

[Table("ligne_commande_fournisseur")]
public class LigneCommandeFournisseurModel
{
    
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    
    [Column("quantite")]
    [Required]
    public int Quantite { get; set; }
    
    [Column("prix_unitaire")]
    [Required]
    public double PrixUnitaire { get; set; }
    
    [Column("remise")]
    [Required]
    public double Remise { get; set; }
    
    [Column("total")]
    [Required]
    public double Total { get; set; }
    
    [Column("produit_id")]
    [Required]
    public int ProduitId { get; set; }
    
    [Column("commande_fournisseur_id")]
    [Required]
    public int CommandeFournisseurId { get; set; }
    
    [ForeignKey("ProduitId")]
    public ProduitModel Produit { get; set; }
    
    [ForeignKey("CommandeFournisseurId")]
    public CommandeFournisseurModel CommandeFournisseur { get; set; }
}