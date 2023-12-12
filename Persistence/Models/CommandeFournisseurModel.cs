using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

[Table("commande_fournisseur")]
public class CommandeFournisseurModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Column("date_commande")] [Required] public required DateTime DateCommande { get; set; }

    [Column("date_reception")] public required DateTime? DateReception { get; set; }

    [Column("statut")]
    [Required]
    [StringLength(50)]
    public required string Statut { get; set; }

    [Column("fournisseur_id")] [Required] public required int FournisseurId { get; set; }

    [Column("employe_id")] [Required] public required int EmployeId { get; set; }

    [ForeignKey("FournisseurId")] public required FournisseurModel Fournisseur { get; set; }

    [ForeignKey("EmployeId")] public required EmployeModel Employe { get; set; }

    public required ICollection<LigneCommandeFournisseurModel> LigneCommandeFournisseurs { get; set; }
}