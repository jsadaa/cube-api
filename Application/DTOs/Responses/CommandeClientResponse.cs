namespace ApiCube.Application.DTOs.Responses;

public class CommandeClientResponse
{
    public int Id { get; set; }
    public DateTime DateCommande { get; set; }
    public DateTime? DateLivraison { get; set; } = null;
    public string Statut { get; set; }
    public ClientResponse Client { get; set; }
    public List<LigneCommandeClientResponse> LigneCommandeClients { get; set; }
}
