using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

[Table("commande_client")]
public class CommandeClientModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Column("date_commande")] [Required] public required DateTime DateCommande { get; set; }

    [Column("date_livraison")] [Required] public required DateTime DateLivraison { get; set; }

    [Column("statut")]
    [Required]
    [StringLength(50)]
    public required string Statut { get; set; }

    [Column("client_id")] [Required] public required int ClientId { get; set; }

    [Column("employe_id")] [Required] public required int EmployeId { get; set; }

    [ForeignKey("ClientId")] public required ClientModel Client { get; set; }

    [ForeignKey("EmployeId")] public required EmployeModel Employe { get; set; }

    public required  ICollection<LigneCommandeClientModel> LigneCommandeClients { get; set; }
}