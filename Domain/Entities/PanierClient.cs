using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Entities;

public class PanierClient
{
    public int Id { get; set; }
    public Client Client { get; set; }
    public ICollection<LignePanierClient> LignePanierClients { get; set; } = new List<LignePanierClient>();
    
    public PanierClient(Client client)
    {
        Client = client;
    }
    
    public PanierClient(int id, Client client)
    {
        Id = id;
        Client = client;
    }
    
    public PanierClient(int id, Client client, ICollection<LignePanierClient> lignePanierClients)
    {
        Id = id;
        Client = client;
        LignePanierClients = lignePanierClients;
    }
    
    public void AjouterLignePanierClient(LignePanierClient lignePanierClient)
    {
        VerifierDoublonProduit(lignePanierClient.Produit);
        LignePanierClients.Add(lignePanierClient);
    }
    
    public void SupprimerLignePanierClient(LignePanierClient lignePanierClient)
    {
        LignePanierClients.Remove(lignePanierClient);
    }
    
    public void ViderPanier()
    {
        LignePanierClients.Clear();
    }
    
    public void VerifierDoublonProduit(Produit produit)
    {
        if (LignePanierClients.Any(lignePanierClient => lignePanierClient.Produit.Id == produit.Id))
        {
            throw new ProduitDejaDansPanier();
        }
    }
}