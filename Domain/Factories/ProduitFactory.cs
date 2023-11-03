using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Repositories.FamilleProduit;
using ApiCube.Persistence.Repositories.Fournisseur;

namespace ApiCube.Domain.Factories;

public class ProduitFactory
{
    private readonly IFamilleProduitRepository _familleProduitRepository;
    private readonly IFournisseurRepository _fournisseurRepository;

    public ProduitFactory(IFamilleProduitRepository familleProduitRepository, IFournisseurRepository fournisseurRepository)
    {
        _familleProduitRepository = familleProduitRepository;
        _fournisseurRepository = fournisseurRepository;
    }
    
    /// <summary>
    /// Créer un produit à partir d'une requête
    /// </summary>
    /// <param name="produitRequest"> La requête </param>
    /// <returns> Produit </returns>
    /// <exception cref="Exception"> Si la famille de produit ou le fournisseur n'existe pas </exception>
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
            nom: produitRequest.Nom,
            description: produitRequest.Description,
            appellation: produitRequest.Appellation,
            cepage: produitRequest.Cepage,
            region: produitRequest.Region,
            degreAlcool: produitRequest.DegreAlcool,
            prixAchat: produitRequest.PrixAchat,
            prixVente: produitRequest.PrixVente,
            enPromotion: produitRequest.EnPromotion,
            familleProduit: familleProduit,
            fournisseur: fournisseur
        );
        
        return nouveauProduit;
    }
    
    public Produit MapperProduit(ProduitDTO produitDTO)
    {
        FamilleProduitDTO? familleProduitDTO = _familleProduitRepository.Trouver(produitDTO.FamilleProduitNom);
        FournisseurDTO? fournisseurDTO = _fournisseurRepository.Trouver(produitDTO.FournisseurNom);
        
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
        
        Produit produit = new Produit(
            id: produitDTO.Id,
            nom: produitDTO.Nom,
            description: produitDTO.Description,
            appellation: produitDTO.Appellation,
            cepage: produitDTO.Cepage,
            region: produitDTO.Region,
            degreAlcool: produitDTO.DegreAlcool,
            prixAchat: produitDTO.PrixAchat,
            prixVente: produitDTO.PrixVente,
            enPromotion: produitDTO.EnPromotion,
            familleProduit: familleProduit,
            fournisseur: fournisseur
        );
        
        return produit;
    }
}