using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Persistence.Models;

[Table("panier_client")]
public class PanierClientModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Column("total")] [Required] public required double Total { get; set; }

    [Column("client_id")] [Required] public required int ClientId { get; set; }

    [ForeignKey("ClientId")] public required ClientModel Client { get; set; }

    public required List<LignePanierClientModel> LignePanierClients { get; set; }
}