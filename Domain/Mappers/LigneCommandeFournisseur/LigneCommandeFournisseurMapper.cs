namespace ApiCube.Domain.Mappers.LigneCommandeFournisseur;

public class LigneCommandeFournisseurMapper : ILigneCommandeFournisseurMapper
{
    public Domain.Entities.LigneCommandeFournisseur Mapper(Persistence.Models.LigneCommandeFournisseurModel ligneCommandeFournisseurModel, Domain.Entities.Produit produit, int commandeFournisseurId)
    {
        return new Domain.Entities.LigneCommandeFournisseur(
            id: ligneCommandeFournisseurModel.Id,
            quantite: ligneCommandeFournisseurModel.Quantite,
            prixUnitaire: ligneCommandeFournisseurModel.PrixUnitaire,
            remise: ligneCommandeFournisseurModel.Remise,
            total: ligneCommandeFournisseurModel.Total,
            produit: produit,
            commandeFournisseurId: commandeFournisseurId
        );
    }
}