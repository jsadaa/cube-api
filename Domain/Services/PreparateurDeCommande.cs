using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Domain.Entities;
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
            dateCommande: DateTime.Now,
            dateReception: null,
            fournisseur: fournisseur,
            statut: StatutCommande.EnCours,
            employe: employe
        );
        
        foreach (var produitCommandeRequest in produits)
        {
            var produit = _produitRepository.Trouver(produitCommandeRequest.ProduitId);
            var nouvelleLigneCommandeFournisseur = new LigneCommandeFournisseur(
                quantite: produitCommandeRequest.Quantite,
                prixUnitaire: produit.PrixAchat,
                remise: 0,
                total: produitCommandeRequest.Quantite * produit.PrixAchat,
                produit: produit,
                commandeFournisseurId: nouvelleCommandeFournisseur.Id
            );
            nouvelleCommandeFournisseur.AjouterLigneCommandeFournisseur(nouvelleLigneCommandeFournisseur);
        }
        
        return nouvelleCommandeFournisseur;
    }
    
    public CommandeFournisseur ModificationInterne(CommandeFournisseur commandeFournisseur, List<ProduitCommandeRequest> produits, Fournisseur fournisseur, Employe employe)
    {
        commandeFournisseur.MettreAJour(
            dateCommande: DateTime.Now,
            fournisseur: fournisseur,
            statut: StatutCommande.EnCours,
            employe: employe
        );
        commandeFournisseur.ViderLignesCommande();
        
        foreach (var produitCommandeRequest in produits)
        {
            var produit = _produitRepository.Trouver(produitCommandeRequest.ProduitId);
            var nouvelleLigneCommandeFournisseur = new LigneCommandeFournisseur(
                quantite: produitCommandeRequest.Quantite,
                prixUnitaire: produit.PrixAchat,
                remise: 0,
                total: produitCommandeRequest.Quantite * produit.PrixAchat,
                produit: produit,
                commandeFournisseurId: commandeFournisseur.Id
            );
            commandeFournisseur.AjouterLigneCommandeFournisseur(nouvelleLigneCommandeFournisseur);
        }
        
        return commandeFournisseur;
    }
    
    
    public CommandeFournisseur Reception(CommandeFournisseur commandeFournisseur)
    {
        commandeFournisseur.DateReception = DateTime.Now;
        commandeFournisseur.Statut = StatutCommande.Livree;
        return commandeFournisseur;
    }
}