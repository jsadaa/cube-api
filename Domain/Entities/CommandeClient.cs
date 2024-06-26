using ApiCube.Domain.Enums.Commande;

namespace ApiCube.Domain.Entities;

public class CommandeClient
{
    public CommandeClient(Guid uuid, DateTime dateCommande, DateTime? dateLivraison, StatutCommande statut,
        Client client)
    {
        Uuid = uuid;
        DateCommande = dateCommande;
        DateLivraison = dateLivraison;
        Statut = statut;
        Client = client;
    }

    public CommandeClient(int id, Guid uuid, DateTime dateCommande, DateTime? dateLivraison, StatutCommande statut,
        Client client)
    {
        Id = id;
        Uuid = uuid;
        DateCommande = dateCommande;
        DateLivraison = dateLivraison;
        Statut = statut;
        Client = client;
    }

    public CommandeClient(int id, Guid uuid, DateTime dateCommande, DateTime? dateLivraison, StatutCommande statut,
        Client client,
        List<LigneCommandeClient> ligneCommandeClients)
    {
        Id = id;
        Uuid = uuid;
        DateCommande = dateCommande;
        DateLivraison = dateLivraison;
        Statut = statut;
        Client = client;
        LigneCommandeClients = ligneCommandeClients;
    }

    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public DateTime DateCommande { get; set; }
    public DateTime? DateLivraison { get; set; }
    public StatutCommande Statut { get; set; }
    public Client Client { get; set; }
    public List<LigneCommandeClient> LigneCommandeClients { get; set; } = new();

    public void AjouterLigneCommandeClient(LigneCommandeClient ligneCommandeClient)
    {
        LigneCommandeClients.Add(ligneCommandeClient);
    }

    public void SupprimerLigneCommandeClient(LigneCommandeClient ligneCommandeClient)
    {
        LigneCommandeClients.Remove(ligneCommandeClient);
    }

    public void ModifierStatut(StatutCommande statut)
    {
        Statut = statut;
    }

    public void ModifierDateLivraison(DateTime dateLivraison)
    {
        DateLivraison = dateLivraison;
    }
}