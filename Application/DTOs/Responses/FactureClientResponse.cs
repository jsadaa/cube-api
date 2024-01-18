namespace ApiCube.Application.DTOs.Responses;

public class FactureClientResponse
{
    public int Id { get; set; }
    public DateTime DateFacture { get; set; }
    public string Statut { get; set; }
    public double PrixHt { get; set; }
    public double PrixTtc { get; set; }
    public double Tva { get; set; }
    public ClientResponse Client { get; set; }
    public CommandeClientResponse CommandeClient { get; set; }
}