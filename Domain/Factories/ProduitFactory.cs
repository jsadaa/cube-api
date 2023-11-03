using ApiCube.Domain.Entities;
using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;
using ApiCube.Repositories.FamilleProduit;
using ApiCube.Repositories.Fournisseur;

namespace ApiCube.Domain.Factories;

public class ProduitFactory
{
    private IFamilleProduitRepository _familleProduitRepository;
    private IFournisseurRepository _fournisseurRepository;

    public ProduitFactory(IFamilleProduitRepository familleProduitRepository, IFournisseurRepository fournisseurRepository)
    {
        _familleProduitRepository = familleProduitRepository;
        _fournisseurRepository = fournisseurRepository;
    }
    
    public Produit CreerProduit(AjouterProduitRequest produitRequest)
    {
        FamilleProduitDTO? familleProduitDTO = _familleProduitRepository.Trouver(produitRequest.FamilleProduitId);
        FournisseurDTO? fournisseurDTO = _fournisseurRepository.Trouver(produitRequest.FournisseurId);
        
        if (familleProduitDTO == null) throw new Exception("La famille de produit n'existe pas");
        if (fournisseurDTO == null) throw new Exception("Le fournisseur n'existe pas");
        
        FamilleProduit familleProduit = new FamilleProduit(
            id: familleProduitDTO.Id,
            nom: familleProduitDTO.Nom,
            description: familleProduitDTO.Description
        );
        
        Fournisseur fournisseur = new Fournisseur(
            id: fournisseurDTO.Id,
            nom: fournisseurDTO.Nom,
            adresse: fournisseurDTO.Adresse,
            telephone: fournisseurDTO.Telephone,
            email: fournisseurDTO.Email
        );
        
        Produit nouveauProduit = new Produit(
            id: 0,
            nom: produitRequest.Nom,
            description: produitRequest.Description,
            quantite: produitRequest.Quantite,
            seuilDisponibilite: produitRequest.SeuilDisponibilite,
            statutStock: produitRequest.StatutStock,
            prixAchat: produitRequest.PrixAchat,
            prixVente: produitRequest.PrixVente,
            dateAchat: produitRequest.DateAchat,
            datePeremption: produitRequest.DatePeremption,
            familleProduit: familleProduit,
            fournisseur: fournisseur
        );
        
        return nouveauProduit;
    }
}