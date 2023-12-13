using ApiCube.Application.DTOs.Requests;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Commande;
using ApiCube.Domain.Enums.Stock;
using ApiCube.Persistence.Repositories.Produit;

namespace ApiCube.Domain.Services;

public class PreparateurDeCommande
{
    private readonly IProduitRepository _produitRepository;

    public PreparateurDeCommande(IProduitRepository produitRepository)
    {
        _produitRepository = produitRepository;
    }

    public CommandeFournisseur Commande(Fournisseur fournisseur, Employe employe, List<ProduitCommandeRequest> produits)
    {
        var nouvelleCommandeFournisseur = new CommandeFournisseur(
            DateTime.Now,
            null,
            fournisseur,
            StatutCommande.EnCours,
            employe
        );

        foreach (var produitCommandeRequest in produits)
        {
            var produit = _produitRepository.Trouver(produitCommandeRequest.ProduitId);
            var nouvelleLigneCommandeFournisseur = new LigneCommandeFournisseur(
                produitCommandeRequest.Quantite,
                produit.PrixAchat,
                0,
                produitCommandeRequest.Quantite * produit.PrixAchat,
                produit,
                nouvelleCommandeFournisseur.Id
            );
            
            // On vérifie la validité de la ligne de commande
            nouvelleLigneCommandeFournisseur.VerifierValidite();
            nouvelleCommandeFournisseur.AjouterLigneCommandeFournisseur(nouvelleLigneCommandeFournisseur);
        }
        
        // On vérifie la validité de la commande
        nouvelleCommandeFournisseur.VerifierValidite();

        return nouvelleCommandeFournisseur;
    }

    public CommandeFournisseur ModificationInterne(CommandeFournisseur commandeFournisseur,
        List<ProduitCommandeRequest> produits, Fournisseur fournisseur, Employe employe)
    {
        commandeFournisseur.MettreAJour(
            DateTime.Now,
            fournisseur,
            StatutCommande.EnCours,
            employe
        );
        commandeFournisseur.ViderLignesCommande();

        foreach (var produitCommandeRequest in produits)
        {
            var produit = _produitRepository.Trouver(produitCommandeRequest.ProduitId);
            var nouvelleLigneCommandeFournisseur = new LigneCommandeFournisseur(
                produitCommandeRequest.Quantite,
                produit.PrixAchat,
                0,
                produitCommandeRequest.Quantite * produit.PrixAchat,
                produit,
                commandeFournisseur.Id
            );
            
            // On vérifie la validité de la ligne de commande
            nouvelleLigneCommandeFournisseur.VerifierValidite();
            commandeFournisseur.AjouterLigneCommandeFournisseur(nouvelleLigneCommandeFournisseur);
        }
        
        // On vérifie la validité de la commande
        commandeFournisseur.VerifierValidite();

        return commandeFournisseur;
    }


    public CommandeFournisseur Reception(CommandeFournisseur commandeFournisseur)
    {
        commandeFournisseur.VerifierValidite();
        commandeFournisseur.Receptionner();
        return commandeFournisseur;
    }
}