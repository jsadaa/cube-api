using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Repositories.FamilleProduit;
using ApiCube.Persistence.Repositories.Fournisseur;
using AutoMapper;

namespace ApiCube.Domain.Factories;

public class ProduitFactory
{
    private readonly IFamilleProduitRepository _familleProduitRepository;
    private readonly IFournisseurRepository _fournisseurRepository;
    private readonly IMapper _mapper;

    public ProduitFactory(IFamilleProduitRepository familleProduitRepository, IFournisseurRepository fournisseurRepository, IMapper mapper)
    {
        _familleProduitRepository = familleProduitRepository;
        _fournisseurRepository = fournisseurRepository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Créer un produit à partir d'une requête
    /// </summary>
    /// <param name="produitRequest"> La requête </param>
    /// <returns> Produit </returns>
    /// <exception cref="Exception"> Si la famille de produit ou le fournisseur n'existe pas </exception>
    public Produit CreerProduit(AjouterProduitRequest produitRequest)
    {
        FamilleProduit familleProduit = _mapper.Map<FamilleProduit>(
            _familleProduitRepository.Trouver(
                produitRequest.FamilleProduitId
            )
        );
        
        Fournisseur fournisseur = _mapper.Map<Fournisseur>(
            _fournisseurRepository.Trouver(
                produitRequest.FournisseurId
            )
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
        FamilleProduit familleProduit = _mapper.Map<FamilleProduit>(
            _familleProduitRepository.Trouver(
                produitDTO.FamilleProduitNom
            )
        );
        
        Fournisseur fournisseur = _mapper.Map<Fournisseur>(
            _fournisseurRepository.Trouver(
                produitDTO.FournisseurNom
            )
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