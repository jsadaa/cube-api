using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

[Table("facture_client")]
public class FactureClientModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Column("date_facture")] [Required] public required DateTime DateFacture { get; set; }

    [Column("statut")]
    [Required]
    [StringLength(50)]
    public required string Statut { get; set; }

    [Column("prix_ht")] [Required] public required double PrixHt { get; set; }

    [Column("prix_ttc")] [Required] public required double PrixTtc { get; set; }

    [Column("tva")] [Required] public required double Tva { get; set; }

    [Column("client_id")] [Required] public required int ClientId { get; set; }

    [ForeignKey("ClientId")] public required ClientModel Client { get; set; }

    [Column("commande_client_id")]
    [Required]
    public required int CommandeClientId { get; set; }

    [ForeignKey("CommandeClientId")] public required CommandeClientModel CommandeClient { get; set; }
}