using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

[Table("employe")]
public class EmployeModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Column("nom")]
    [Required]
    [StringLength(50)]
    public required string Nom { get; set; }

    [Column("prenom")]
    [Required]
    [StringLength(50)]
    public required string Prenom { get; set; }

    [Column("email")]
    [Required]
    [StringLength(50)]
    public required string Email { get; set; }

    [Column("date_embauche")] [Required] public required DateTime DateEmbauche { get; set; }

    [Column("date_depart")] public DateTime? DateDepart { get; set; }

    [Column("statut")]
    [Required]
    [StringLength(50)]
    public required string Statut { get; set; }

    [Column("application_user_id")]
    [Required]
    public required string ApplicationUserId { get; set; }

    [ForeignKey("ApplicationUserId")] public ApplicationUserModel ApplicationUser { get; set; } = null!;
}