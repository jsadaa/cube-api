using ApiCube.Application.DTOs.Requests;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
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
    
    public Produit Creer(ProduitRequestDTO produitRequestDTO)
    {
        FamilleProduit familleProduit = _familleProduitRepository.Trouver(produitRequestDTO.FamilleProduitId);
        Fournisseur fournisseur = _fournisseurRepository.Trouver(produitRequestDTO.FournisseurId);
        
        Produit nouveauProduit = new Produit(
            nom: produitRequestDTO.Nom,
            description: produitRequestDTO.Description,
            appellation: produitRequestDTO.Appellation,
            cepage: produitRequestDTO.Cepage,
            region: produitRequestDTO.Region,
            degreAlcool: produitRequestDTO.DegreAlcool,
            prixAchat: produitRequestDTO.PrixAchat,
            prixVente: produitRequestDTO.PrixVente,
            enPromotion: produitRequestDTO.EnPromotion,
            familleProduit: familleProduit,
            fournisseur: fournisseur
        );
        
        return nouveauProduit;
    }
    
    public Produit Mapper(ProduitModel produitModel)
    {
        FamilleProduit familleProduit = _familleProduitRepository.Trouver(produitModel.FamilleProduitId);
        Fournisseur fournisseur = _fournisseurRepository.Trouver(produitModel.FournisseurId);
        
        Produit produit = new Produit(
            id: produitModel.Id,
            nom: produitModel.Nom,
            description: produitModel.Description,
            appellation: produitModel.Appellation,
            cepage: produitModel.Cepage,
            region: produitModel.Region,
            degreAlcool: produitModel.DegreAlcool,
            prixAchat: produitModel.PrixAchat,
            prixVente: produitModel.PrixVente,
            enPromotion: produitModel.EnPromotion,
            familleProduit: familleProduit,
            fournisseur: fournisseur
        );
        
        return produit;
    }
}