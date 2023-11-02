using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Models;

[Table("famille_produit")]
public class FamilleProduitModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    
    [Column("nom")]
    [Required]
    [StringLength(50)]
    public string Nom { get; set; }
    
    [Column("description")]
    [Required]
    [StringLength(200)]
    public string Description { get; set; }
}