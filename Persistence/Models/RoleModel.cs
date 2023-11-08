using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

[Table("role")]
public class RoleModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Column("nom")]
    [Required]
    [StringLength(50)]
    public required string Nom { get; set; }

    public ICollection<EmployeModel>? Employes { get; set; }
}