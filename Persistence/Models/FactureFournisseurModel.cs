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

    [Column("date_facture")] [Required] public DateTime DateFacture { get; set; }

    [Column("date_echeance")] [Required] public DateTime DateEcheance { get; set; }

    [Column("statut")]
    [Required]
    [StringLength(50)]
    public string Statut { get; set; }

    [Column("prix_ht")] [Required] public double PrixHt { get; set; }

    [Column("prix_ttc")] [Required] public double PrixTtc { get; set; }

    [Column("tva")] [Required] public double Tva { get; set; }

    [Column("fournisseur_id")] [Required] public int FournisseurId { get; set; }

    [Column("employe_id")] [Required] public int EmployeId { get; set; }

    [ForeignKey("FournisseurId")] public FournisseurModel Fournisseur { get; set; }

    [ForeignKey("EmployeId")] public EmployeModel Employe { get; set; }

    [Column("commande_fournisseur_id")]
    [Required]
    public int CommandeFournisseurId { get; set; }

    [ForeignKey("CommandeFournisseurId")] public CommandeFournisseurModel CommandeFournisseur { get; set; }
}