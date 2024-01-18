using ApiCube.Domain.Enums.Facture;

namespace ApiCube.Domain.Entities;

public class FactureClient
{
    public FactureClient(int id, DateTime dateFacture, StatutFacture statut, double tva, Client client,
        CommandeClient commandeClient)
    {
        Id = id;
        DateFacture = dateFacture;
        Statut = statut;
        Tva = tva;
        PrixHt = CalculerPrixHt();
        PrixTtc = CalculerPrixTtc();
        Client = client;
        CommandeClient = commandeClient;
    }

    public FactureClient(DateTime dateFacture, StatutFacture statut, double tva, Client client,
        CommandeClient commandeClient)
    {
        DateFacture = dateFacture;
        Statut = statut;
        Tva = tva;
        PrixHt = CalculerPrixHt();
        PrixTtc = CalculerPrixTtc();
        Client = client;
        CommandeClient = commandeClient;
    }

    public int Id { get; set; }
    public DateTime DateFacture { get; set; }
    public StatutFacture Statut { get; set; }
    public double PrixHt { get; set; }
    public double PrixTtc { get; set; }
    public double Tva { get; set; }
    public Client Client { get; set; }
    public CommandeClient CommandeClient { get; set; }

    private double CalculerPrixHt()
    {
        return CommandeClient.LigneCommandeClients.Sum(ligneCommandeClient =>
            ligneCommandeClient.Produit.PrixVente * ligneCommandeClient.Quantite);
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