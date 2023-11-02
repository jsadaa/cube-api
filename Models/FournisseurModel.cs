using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Models;

[Table("fournisseur")]
public class FournisseurModel
{
    
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    
    [Column("nom")]
    [Required]
    [StringLength(50)]
    public string Nom { get; set; }
    
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
    
    public ICollection<ProduitModel> Produits { get; set; }
    
    /*// a des Commandes
    public List<CommandeModel> Commandes { get; set; }

    // a des Factures
    public List<FactureModel> Factures { get; set; }

    // a des Paiements
    public List<PaiementModel> Paiements { get; set; }

    // a des Retours
    public List<RetourModel> Retours { get; set; }

    // a des Avoirs
    public List<AvoirModel> Avoirs { get; set; }

    // a des Acomptes
    public List<AcompteModel> Acomptes { get; set; }

    // a des Acomptes
    public List<RemboursementModel> Remboursements { get; set; }

    // a des Acomptes
    public List<RemiseModel> Remises { get;*/
}