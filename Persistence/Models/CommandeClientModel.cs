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

    [Column("uuid")]
    [Required]
    [StringLength(36)]
    public required Guid Uuid { get; set; }

    [Column("date_commande")] [Required] public required DateTime DateCommande { get; set; }

    [Column("date_livraison")] public required DateTime? DateLivraison { get; set; }

    [Column("statut")]
    [Required]
    [StringLength(50)]
    public required string Statut { get; set; }

    [Column("client_id")] [Required] public required int ClientId { get; set; }

    [ForeignKey("ClientId")] public required ClientModel Client { get; set; }

    public required List<LigneCommandeClientModel> LigneCommandeClients { get; set; }
}