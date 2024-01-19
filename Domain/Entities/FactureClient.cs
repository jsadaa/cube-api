using ApiCube.Domain.Enums.Facture;

namespace ApiCube.Domain.Entities;

public class FactureClient
{
    public FactureClient(int id, DateTime dateFacture, StatutFacture statut, double tva,
        CommandeClient commandeClient)
    {
        Id = id;
        DateFacture = dateFacture;
        Statut = statut;
        Tva = tva;
        CommandeClient = commandeClient;
        PrixHt = CalculerPrixHt();
        PrixTtc = CalculerPrixTtc();
    }

    public FactureClient(DateTime dateFacture, StatutFacture statut, double tva,
        CommandeClient commandeClient)
    {
        DateFacture = dateFacture;
        Statut = statut;
        Tva = tva;
        CommandeClient = commandeClient;
        PrixHt = CalculerPrixHt();
        PrixTtc = CalculerPrixTtc();
    }

    public int Id { get; set; }
    public DateTime DateFacture { get; set; }
    public StatutFacture Statut { get; set; }
    public double PrixHt { get; set; }
    public double PrixTtc { get; set; }
    public double Tva { get; set; }
    public CommandeClient CommandeClient { get; set; }

    private double CalculerPrixHt()
    {
        return CommandeClient.LigneCommandeClients.Sum(ligneCommandeClient => ligneCommandeClient.Total);
    }

    private double CalculerPrixTtc()
    {
        return CalculerPrixHt() * (1 + Tva);
    }

    public void MettreAJoutStatut(StatutFacture statut)
    {
        Statut = statut;
    }
}