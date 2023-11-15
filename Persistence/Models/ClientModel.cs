using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ApiCube.Persistence.Models;

[Table("client")]
public class ClientModel : IdentityUser
{
    //[Column("id")]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //[Key]
    //public int Id { get; set; }

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

    /*[Column("email")]
    [Required]
    [StringLength(50)]
    public required string Email { get; set; }

    [Column("password")]
    [Required]
    [StringLength(50)]
    public required string Password { get; set; }*/
    
    [Column("refresh_token")]
    [Required]
    [StringLength(500)]
    public string RefreshToken { get; set; }

    [Column("date_naissance")] [Required] public DateTime DateNaissance { get; set; }

    [Column("date_inscription")]
    [Required]
    public required DateTime DateInscription { get; set; }

    [Column("statut")]
    [Required]
    [StringLength(50)]
    public required string Statut { get; set; }

    [Column("solde")] [Required] public required double Solde { get; set; }

    [Column("points_fidelite")] [Required] public required int PointsFidelite { get; set; }

    [Column("role")]
    [Required]
    [StringLength(50)]
    public required string Role { get; set; }

    public ICollection<CommandeClientModel>? Commandes { get; set; }

    public ICollection<FactureClientModel>? Factures { get; set; }
}