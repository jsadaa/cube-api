namespace ApiCube.Application.DTOs.Responses;

public class CommandeFournisseurResponse
{
    public int Id { get; set; }
    public DateTime DateCommande { get; set; }
    public DateTime DateReception { get; set; }
    public string Statut { get; set; }
    public FournisseurResponse Fournisseur { get; set; }
    public EmployeResponse Employe { get; set; }
    public ICollection<LigneCommandeFournisseurResponse> LigneCommandeFournisseurs { get; set; }
}