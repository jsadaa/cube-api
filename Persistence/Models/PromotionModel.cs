using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

[Table("promotion")]
public class PromotionModel
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

    [Column("date_debut")] [Required] public DateTime DateDebut { get; set; }

    [Column("date_fin")] [Required] public DateTime DateFin { get; set; }

    [Column("pourcentage")] [Required] public double Pourcentage { get; set; }
}