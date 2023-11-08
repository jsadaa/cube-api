using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

[Table("facture_fournisseur")]
public class FactureFournisseurModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Column("date_facture")] [Required] public required DateTime DateFacture { get; set; }

    [Column("date_echeance")] [Required] public required DateTime DateEcheance { get; set; }

    [Column("statut")]
    [Required]
    [StringLength(50)]
    public required string Statut { get; set; }

    [Column("prix_ht")] [Required] public required double PrixHt { get; set; }

    [Column("prix_ttc")] [Required] public required double PrixTtc { get; set; }

    [Column("tva")] [Required] public required double Tva { get; set; }

    [Column("fournisseur_id")] [Required] public required int FournisseurId { get; set; }

    [Column("employe_id")] [Required] public required int EmployeId { get; set; }

    [ForeignKey("FournisseurId")] public required FournisseurModel Fournisseur { get; set; }

    [ForeignKey("EmployeId")] public required EmployeModel Employe { get; set; }

    [Column("commande_fournisseur_id")]
    [Required]
    public required int CommandeFournisseurId { get; set; }

    [ForeignKey("CommandeFournisseurId")] public required CommandeFournisseurModel CommandeFournisseur { get; set; }
}