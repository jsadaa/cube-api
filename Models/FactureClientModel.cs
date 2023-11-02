using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCube.Models;

[Table("facture_client")]
public class FactureClientModel
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    
    [Column("date_facture")]
    [Required]
    public DateTime DateFacture { get; set; }
    
    [Column("statut")]
    [Required]
    [StringLength(50)]
    public string Statut { get; set; }
    
    [Column("prix_ht")]
    [Required]
    public double PrixHt { get; set; }
    
    [Column("prix_ttc")]
    [Required]
    public double PrixTtc { get; set; }
    
    [Column("tva")]
    [Required]
    public double Tva { get; set; }
    
    [Column("commande_client_id")]
    [Required]
    public int CommandeClientId { get; set; }
    
    [ForeignKey("CommandeClientId")]
    public CommandeClientModel CommandeClient { get; set; }
}