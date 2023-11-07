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
    public string Nom { get; set; }

    [Column("prenom")]
    [Required]
    [StringLength(50)]
    public string Prenom { get; set; }

    [Column("email")]
    [Required]
    [StringLength(50)]
    public string Email { get; set; }

    [Column("mot_de_passe")]
    [Required]
    [StringLength(50)]
    public string MotDePasse { get; set; }

    [Column("date_embauche")] [Required] public DateTime DateEmbauche { get; set; }

    [Column("date_depart")] [Required] public DateTime DateDepart { get; set; }

    [Column("statut")]
    [Required]
    [StringLength(50)]
    public string Statut { get; set; }

    [Column("role")]
    [Required]
    [StringLength(50)]
    public string Role { get; set; }
}