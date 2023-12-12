using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.LigneCommandeFournisseur;

public interface ILigneCommandeFournisseurMapper
{
    public Domain.Entities.LigneCommandeFournisseur Mapper(LigneCommandeFournisseurModel ligneCommandeFournisseurModel, Domain.Entities.Produit produit, int commandeFournisseurId);
}