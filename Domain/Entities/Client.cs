using ApiCube.Domain.ValuesObjects;

namespace ApiCube.Domain.Entities;

public class Client
{
    public Client(int id, string username, string nom, string prenom, string adresse, string codePostal, string ville,
        string pays, string telephone, string email, DateTime dateNaissance,
        DateTime dateInscription, string applicationUserId)
    {
        Id = id;
        Username = username;
        Nom = nom;
        Prenom = prenom;
        Adresse = new Adresse(adresse, codePostal, ville, pays);
        Telephone = new Telephone(telephone);
        Email = email;
        DateNaissance = dateNaissance;
        DateInscription = dateInscription;
        ApplicationUserId = applicationUserId;
    }

    public Client(string username, string nom, string prenom, string adresse, string codePostal, string ville,
        string pays, string telephone, string email, DateTime dateNaissance,
        DateTime dateInscription, string applicationUserId)
    {
        Username = username;
        Nom = nom;
        Prenom = prenom;
        Adresse = new Adresse(adresse, codePostal, ville, pays);
        Telephone = new Telephone(telephone);
        Email = email;
        DateNaissance = dateNaissance;
        DateInscription = dateInscription;
        ApplicationUserId = applicationUserId;
    }

    public Client(string username, string nom, string prenom, string adresse, string codePostal, string ville,
        string pays, string telephone, string email, DateTime dateNaissance,
        DateTime dateInscription, List<CommandeClient> commandes, List<FactureClient> factures,
        List<PanierClient> paniers, string applicationUserId)
    {
        Username = username;
        Nom = nom;
        Prenom = prenom;
        Adresse = new Adresse(adresse, codePostal, ville, pays);
        Telephone = new Telephone(telephone);
        Email = email;
        DateNaissance = dateNaissance;
        DateInscription = dateInscription;
        Commandes = commandes;
        Factures = factures;
        Paniers = paniers;
        ApplicationUserId = applicationUserId;
    }

    public Client(int id, string username, string nom, string prenom, string adresse, string codePostal, string ville,
        string pays, string telephone, string email, DateTime dateNaissance,
        DateTime dateInscription, List<CommandeClient> commandes, List<FactureClient> factures,
        List<PanierClient> paniers, string applicationUserId)
    {
        Id = id;
        Username = username;
        Nom = nom;
        Prenom = prenom;
        Adresse = new Adresse(adresse, codePostal, ville, pays);
        Telephone = new Telephone(telephone);
        Email = email;
        DateNaissance = dateNaissance;
        DateInscription = dateInscription;
        Commandes = commandes;
        Factures = factures;
        Paniers = paniers;
        ApplicationUserId = applicationUserId;
    }

    public int Id { get; set; }
    public string Username { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public Adresse Adresse { get; set; }
    public Telephone Telephone { get; set; }
    public string Email { get; set; }
    public DateTime DateNaissance { get; set; }
    public DateTime DateInscription { get; set; }
    public List<CommandeClient>? Commandes { get; set; }
    public List<FactureClient>? Factures { get; set; }
    public List<PanierClient>? Paniers { get; set; }
    public string ApplicationUserId { get; set; }

    public void AjouterCommande(CommandeClient commande)
    {
        Commandes ??= new List<CommandeClient>();
        Commandes.Add(commande);
    }

    public void AjouterFacture(FactureClient factureClient)
    {
        Factures ??= new List<FactureClient>();
        Factures.Add(factureClient);
    }

    public void SupprimerCommande(CommandeClient commande)
    {
        Commandes?.Remove(commande);
    }

    public void SupprimerFacture(FactureClient factureClient)
    {
        Factures?.Remove(factureClient);
    }

    public void AjouterPanier(PanierClient panier)
    {
        Paniers ??= new List<PanierClient>();
        Paniers.Add(panier);
    }

    public void SupprimerPanier(PanierClient panier)
    {
        Paniers?.Remove(panier);
    }

    public void MettreAJour(string nom, string prenom, string adresse, string codePostal, string ville, string pays,
        string telephone, string email, DateTime dateNaissance, string applicationUserId)
    {
        Nom = nom;
        Prenom = prenom;
        Adresse = new Adresse(adresse, codePostal, ville, pays);
        Telephone = new Telephone(telephone);
        Email = email;
        DateNaissance = dateNaissance;
        ApplicationUserId = applicationUserId;
    }
}