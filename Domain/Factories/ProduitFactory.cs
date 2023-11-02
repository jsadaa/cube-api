using ApiCube.Domain.Entities;
using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;
using ApiCube.Repositories.Interfaces;

namespace ApiCube.Domain.Factories;

public class ProduitFactory
{
    private IFamilleProduitRepository _familleProduitRepository;

    public ProduitFactory(IFamilleProduitRepository familleProduitRepository)
    {
        _familleProduitRepository = familleProduitRepository;
    }
    
    public Produit CreerProduit(AjouterProduitRequest produitRequest)
    {
        FamilleProduitDTO? familleProduitDTO = _familleProduitRepository.Trouver(produitRequest.FamilleProduitId);
        
        if (familleProduitDTO == null)
        {
            throw new Exception("La famille de produit n'existe pas");
        }
        
        FamilleProduit familleProduit = new FamilleProduit(
            id: familleProduitDTO.Id,
            nom: familleProduitDTO.Nom,
            description: familleProduitDTO.Description
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
            familleProduit: familleProduit
        );
        
        return nouveauProduit;
    }
}