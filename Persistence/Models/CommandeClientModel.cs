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
    
    [Column("date_commande")]
    [Required]
    public DateTime DateCommande { get; set; }
    
    [Column("date_livraison")]
    [Required]
    public DateTime DateLivraison { get; set; }
    
    [Column("statut")]
    [Required]
    [StringLength(50)]
    public string Statut { get; set; }
    
    [Column("client_id")]
    [Required]
    public int ClientId { get; set; }
    
    [Column("employe_id")]
    [Required]
    public int EmployeId { get; set; }
    
    [ForeignKey("ClientId")]
    public ClientModel Client { get; set; }
    
    [ForeignKey("EmployeId")]
    public EmployeModel Employe { get; set; }
    
    public ICollection<LigneCommandeClientModel> LigneCommandeClients { get; set; }
}