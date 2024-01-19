using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Entities;

public class PanierClient
{
    public PanierClient(Client client)
    {
        Client = client;
        Total = CalculerTotal();
    }

    public PanierClient(int id, Client client)
    {
        Id = id;
        Client = client;
        Total = CalculerTotal();
    }

    public PanierClient(int id, Client client, ICollection<LignePanierClient> lignePanierClients)
    {
        Id = id;
        Client = client;
        LignePanierClients = lignePanierClients;
        Total = CalculerTotal();
    }

    public int Id { get; set; }
    public Client Client { get; set; }
    public double Total { get; set; }
    public ICollection<LignePanierClient> LignePanierClients { get; set; } = new List<LignePanierClient>();

    public void AjouterLignePanierClient(LignePanierClient lignePanierClient)
    {
        VerifierDoublonProduit(lignePanierClient.Produit);
        LignePanierClients.Add(lignePanierClient);
        Total = CalculerTotal();
    }

    public void SupprimerLignePanierClient(LignePanierClient lignePanierClient)
    {
        LignePanierClients.Remove(lignePanierClient);
        Total = CalculerTotal();
    }

    public void Vider()
    {
        LignePanierClients.Clear();
    }

    public void VerifierDoublonProduit(Produit produit)
    {
        if (LignePanierClients.Any(lignePanierClient => lignePanierClient.Produit.Id == produit.Id))
            throw new ProduitDejaDansPanier();
    }

    public double CalculerTotal()
    {
        if (LignePanierClients.Count == 0) return 0;
        return LignePanierClients.Sum(lignePanierClient =>
            lignePanierClient.Produit.PrixVente * lignePanierClient.Quantite);
    }
}