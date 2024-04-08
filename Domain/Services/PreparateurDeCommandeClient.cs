using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Commande;

namespace ApiCube.Domain.Services;

public class PreparateurDeCommandeClient
{
    public CommandeClient Commande(PanierClient panierClient)
    {
        var nouvelleCommandeClient = new CommandeClient(
            Guid.NewGuid(),
            DateTime.Now,
            null,
            StatutCommande.EnCours,
            panierClient.Client
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