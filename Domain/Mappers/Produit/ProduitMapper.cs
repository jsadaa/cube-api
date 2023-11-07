using ApiCube.Application.DTOs.Requests;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Mappers.Fournisseur;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Domain.Mappers.Produit;

public class ProduitMapper : IProduitMapper
{
    public Entities.Produit Mapper(ProduitRequestDTO produitRequestDTO, Entities.FamilleProduit familleProduit, Entities.Fournisseur fournisseur)
    {
        var produit = new Entities.Produit(
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
        
        return produit;
    }
    
    public Entities.Produit Mapper(ProduitModel produitModel, Entities.FamilleProduit familleProduit, Entities.Fournisseur fournisseur)
    {
        var produit = new Entities.Produit(
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