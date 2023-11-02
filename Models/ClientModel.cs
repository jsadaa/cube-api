using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Models;

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
    
    [Column("telephone")]
    [Required]
    [StringLength(50)]
    public string Telephone { get; set; }
    
    [Column("email")]
    [Required]
    [StringLength(50)]
    public string Email { get; set; }
    
    [Column("date_naissance")]
    [Required]
    public DateTime DateNaissance { get; set; }
    
    [Column("date_inscription")]
    [Required]
    public DateTime DateInscription { get; set; }
    
    [Column("statut")]
    [Required]
    [StringLength(50)]
    public string Statut { get; set; }
    
    [Column("login")]
    [Required]
    [StringLength(50)]
    public string Login { get; set; }
    
    [Column("password")]
    [Required]
    [StringLength(50)]
    public string Password { get; set; }
    
    [Column("solde")]
    [Required]
    public double Solde { get; set; }
    
    [Column("points_fidelite")]
    [Required]
    public int PointsFidelite { get; set; }
    
    [Column("role")]
    [Required]
    [StringLength(50)]
    public string Role { get; set; }
    
    // a des Commandes
    public ICollection<CommandeClientModel> Commandes { get; set; }
    
    // a des Factures
    public ICollection<FactureClientModel> Factures { get; set; }
    
    // a des Paiements
    /*public List<PaiementModel> Paiements { get; set; }
    
    // a des Retours
    public List<RetourModel> Retours { get; set; }
    
    // a des Avoirs
    public List<AvoirModel> Avoirs { get; set; }*/
}