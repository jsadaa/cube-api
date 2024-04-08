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
    public required string Nom { get; set; }

    [Column("prenom")]
    [Required]
    [StringLength(50)]
    public required string Prenom { get; set; }

    [Column("adresse")]
    [Required]
    [StringLength(200)]
    public required string Adresse { get; set; }

    [Column("code_postal")]
    [Required]
    [StringLength(10)]
    public required string CodePostal { get; set; }

    [Column("ville")]
    [Required]
    [StringLength(50)]
    public required string Ville { get; set; }

    [Column("pays")]
    [Required]
    [StringLength(100)]
    public required string Pays { get; set; }

    [Column("telephone")]
    [Required]
    [StringLength(50)]
    public required string Telephone { get; set; }

    [Column("email")]
    [Required]
    [StringLength(50)]
    public required string Email { get; set; }

    [Column("date_naissance")] [Required] public DateTime DateNaissance { get; set; }

    [Column("date_inscription")]
    [Required]
    public required DateTime DateInscription { get; set; }

    [Column("application_user_id")]
    [Required]
    public required string ApplicationUserId { get; set; }

    [ForeignKey("ApplicationUserId")] public ApplicationUserModel ApplicationUser { get; set; } = null!;

    public List<CommandeClientModel>? Commandes { get; set; }

    public PanierClientModel? Panier { get; set; }

    public List<FactureClientModel>? Factures { get; set; }
}