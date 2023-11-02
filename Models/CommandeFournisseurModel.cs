using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Models;

[Table("commande_fournisseur")]
public class CommandeFournisseurModel
{
    
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    
    [Column("date_commande")]
    [Required]
    public DateTime DateCommande { get; set; }
    
    [Column("date_livraison")]
    [Required]
    public DateTime DateLivraison { get; set; }
    
    [Column("statut")]
    [Required]
    [StringLength(50)]
    public string Statut { get; set; }
    
    [Column("fournisseur_id")]
    [Required]
    public int FournisseurId { get; set; }
    
    [Column("employe_id")]
    [Required]
    public int EmployeId { get; set; }
    
    [ForeignKey("FournisseurId")]
    public FournisseurModel Fournisseur { get; set; }
    
    [ForeignKey("EmployeId")]
    public EmployeModel Employe { get; set; }
    
    public ICollection<LigneCommandeFournisseurModel> LigneCommandeFournisseurs { get; set; }
}