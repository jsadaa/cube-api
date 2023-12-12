using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.LigneCommandeFournisseur;

public interface ILigneCommandeFournisseurMapper
{
    public Entities.LigneCommandeFournisseur Mapper(LigneCommandeFournisseurModel ligneCommandeFournisseurModel,
        Entities.Produit produit, int commandeFournisseurId);
}