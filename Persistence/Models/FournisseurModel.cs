using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

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

    public ICollection<ProduitModel> Produits { get; set; }
}