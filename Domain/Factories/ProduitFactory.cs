using ApiCube.Domain.Entities;
using ApiCube.DTOs;
using ApiCube.Repositories.Interfaces;

namespace ApiCube.Domain.Factories;

public class ProduitFactory
{
    private IFamilleProduitRepository _familleProduitRepository;
    private IPromotionRepository _promotionRepository;
    
    public ProduitFactory(IFamilleProduitRepository familleProduitRepository, IPromotionRepository promotionRepository)
    {
        _familleProduitRepository = familleProduitRepository;
        _promotionRepository = promotionRepository;
    }
    
    public Produit CreerProduit(AjouterProduitRequest produitRequest)
    {
        FamilleProduitDTO? familleProduitDTO = _familleProduitRepository.TrouverFamilleProduit(produitRequest.FamilleProduitId);
        
        if (familleProduitDTO == null)
        {
            throw new Exception("La famille de produit n'existe pas");
        }
        
        FamilleProduit familleProduit = new FamilleProduit(
            nom: familleProduitDTO.Nom,
            description: familleProduitDTO.Description
        );
        
        Produit nouveauProduit = new Produit(
            nom: produitRequest.Nom,
            description: produitRequest.Description,
            quantite: produitRequest.Quantite,
            seuilDisponibilite: produitRequest.SeuilDisponibilite,
            statutStock: produitRequest.StatutStock,
            prixAchat: produitRequest.PrixAchat,
            prixVente: produitRequest.PrixVente,
            dateAchat: produitRequest.DateAchat,
            datePeremption: produitRequest.DatePeremption,
            familleProduit: familleProduit
        );
        
        return nouveauProduit;
    }
}