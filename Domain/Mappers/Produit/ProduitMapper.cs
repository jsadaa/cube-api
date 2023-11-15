using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Produit;

public class ProduitMapper : IProduitMapper
{
    public Entities.Produit Mapper(ProduitModel produitModel, Entities.FamilleProduit familleProduit,
        Entities.Fournisseur fournisseur)
    {
        var produit = new Entities.Produit(
            id: produitModel.Id,
            nom: produitModel.Nom,
            description: produitModel.Description,
            appellation: produitModel.Appellation,
            cepage: produitModel.Cepage,
            region: produitModel.Region,
            annee: produitModel.Annee,
            degreAlcool: produitModel.DegreAlcool,
            prixAchat: produitModel.PrixAchat,
            prixVente: produitModel.PrixVente,
            datePeremption: produitModel.DatePeremption,
            enPromotion: produitModel.EnPromotion,
            familleProduit: familleProduit,
            fournisseur: fournisseur
        );

        return produit;
    }

    public Entities.Produit Mapper(ProduitModel produitModel, Entities.FamilleProduit familleProduit,
        Entities.Fournisseur fournisseur, Entities.Promotion promotion)
    {
        var produit = new Entities.Produit(
            id: produitModel.Id,
            nom: produitModel.Nom,
            description: produitModel.Description,
            appellation: produitModel.Appellation,
            cepage: produitModel.Cepage,
            region: produitModel.Region,
            annee: produitModel.Annee,
            degreAlcool: produitModel.DegreAlcool,
            prixAchat: produitModel.PrixAchat,
            prixVente: produitModel.PrixVente,
            datePeremption: produitModel.DatePeremption,
            enPromotion: produitModel.EnPromotion,
            familleProduit: familleProduit,
            fournisseur: fournisseur,
            promotion: promotion
        );

        return produit;
    }
}