using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Commande;
using ApiCube.Persistence.Repositories.Produit;

namespace ApiCube.Domain.Services;

public class PreparateurDeCommandeClient
{
    private readonly IProduitRepository _produitRepository;

    public PreparateurDeCommandeClient(IProduitRepository produitRepository)
    {
        _produitRepository = produitRepository;
    }

    public CommandeClient Commande(PanierClient panierClient, Client client)
    {
        var nouvelleCommandeClient = new CommandeClient(
            DateTime.Now,
            null,
            StatutCommande.EnCours,
            client
        );

        foreach (var lignePanierClient in panierClient.LignePanierClients)
        {
            var nouvelleLigneCommandeClient = new LigneCommandeClient(
                lignePanierClient.Quantite,
                lignePanierClient.Produit
            );

            nouvelleCommandeClient.AjouterLigneCommandeClient(nouvelleLigneCommandeClient);
        }

        return nouvelleCommandeClient;
    }
}