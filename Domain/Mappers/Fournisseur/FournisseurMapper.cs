using ApiCube.Application.DTOs.Requests;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Fournisseur;

public class FournisseurMapper : IFournisseurMapper
{
    public Entities.Fournisseur Mapper(FournisseurRequestDTO fournisseurRequestDTO)
    {
        return new Entities.Fournisseur(
            nom: fournisseurRequestDTO.Nom,
            adresse: fournisseurRequestDTO.Adresse,
            telephone: fournisseurRequestDTO.Telephone,
            email: fournisseurRequestDTO.Email
        );
    }
    public Entities.Fournisseur Mapper(FournisseurModel fournisseurModel)
    {
        return new Entities.Fournisseur(
            id: fournisseurModel.Id,
            nom: fournisseurModel.Nom,
            adresse: fournisseurModel.Adresse,
            telephone: fournisseurModel.Telephone,
            email: fournisseurModel.Email
        );
    }
}