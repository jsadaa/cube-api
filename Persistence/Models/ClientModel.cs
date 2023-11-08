using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

[Table("client")]
public class ClientModel
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

    [Column("adresse")]
    [Required]
    [StringLength(200)]
    public string Adresse { get; set; }

    [Column("code_postal")]
    [Required]
    [StringLength(10)]
    public string CodePostal { get; set; }

    [Column("ville")]
    [Required]
    [StringLength(50)]
    public string Ville { get; set; }

    [Column("pays")]
    [Required]
    [StringLength(100)]
    public string Pays { get; set; }

    [Column("telephone")]
    [Required]
    [StringLength(50)]
    public string Telephone { get; set; }

    [Column("email")]
    [Required]
    [StringLength(50)]
    public string Email { get; set; }

    [Column("password")]
    [Required]
    [StringLength(50)]
    public string Password { get; set; }

    [Column("date_naissance")] [Required] public DateTime DateNaissance { get; set; }

    [Column("date_inscription")]
    [Required]
    public DateTime DateInscription { get; set; }

    [Column("statut")]
    [Required]
    [StringLength(50)]
    public string Statut { get; set; }

    [Column("solde")] [Required] public double Solde { get; set; }

    [Column("points_fidelite")] [Required] public int PointsFidelite { get; set; }

    [Column("role")]
    [Required]
    [StringLength(50)]
    public string Role { get; set; }

    public ICollection<CommandeClientModel> Commandes { get; set; }

    public ICollection<FactureClientModel> Factures { get; set; }
}