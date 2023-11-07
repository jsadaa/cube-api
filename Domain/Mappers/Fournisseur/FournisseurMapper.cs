using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Fournisseur;

public class FournisseurMapper : IFournisseurMapper
{
    public Entities.Fournisseur Mapper(FournisseurModel fournisseurModel)
    {
        return new Entities.Fournisseur(
            id: fournisseurModel.Id,
            nom: fournisseurModel.Nom,
            adresse: fournisseurModel.Adresse,
            codePostal: fournisseurModel.CodePostal,
            ville: fournisseurModel.Ville,
            pays: fournisseurModel.Pays,
            telephone: fournisseurModel.Telephone,
            email: fournisseurModel.Email
        );
    }
}