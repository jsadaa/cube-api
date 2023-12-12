using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Fournisseur;

public class FournisseurMapper : IFournisseurMapper
{
    public Entities.Fournisseur Mapper(FournisseurModel fournisseurModel)
    {
        return new Entities.Fournisseur(
            fournisseurModel.Id,
            fournisseurModel.Nom,
            fournisseurModel.Adresse,
            fournisseurModel.CodePostal,
            fournisseurModel.Ville,
            fournisseurModel.Pays,
            fournisseurModel.Telephone,
            fournisseurModel.Email
        );
    }
}