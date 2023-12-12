using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.LigneCommandeFournisseur;

public class LigneCommandeFournisseurMapper : ILigneCommandeFournisseurMapper
{
    public Entities.LigneCommandeFournisseur Mapper(LigneCommandeFournisseurModel ligneCommandeFournisseurModel,
        Entities.Produit produit, int commandeFournisseurId)
    {
        return new Entities.LigneCommandeFournisseur(
            ligneCommandeFournisseurModel.Id,
            ligneCommandeFournisseurModel.Quantite,
            ligneCommandeFournisseurModel.PrixUnitaire,
            ligneCommandeFournisseurModel.Remise,
            ligneCommandeFournisseurModel.Total,
            produit,
            commandeFournisseurId
        );
    }
}