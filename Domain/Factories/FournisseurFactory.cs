using ApiCube.Application.DTOs.Requests;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Factories;

public class FournisseurFactory
{
    
    public Fournisseur Creer(FournisseurRequestDTO fournisseurRequestDTO)
    {
        return new Fournisseur(
            id: 0,
            nom: fournisseurRequestDTO.Nom,
            adresse: fournisseurRequestDTO.Adresse,
            telephone: fournisseurRequestDTO.Telephone,
            email: fournisseurRequestDTO.Email
        );
    }
    
    public Fournisseur Mapper(FournisseurModel fournisseurModel)
    {
        return new Fournisseur(
            id: fournisseurModel.Id,
            nom: fournisseurModel.Nom,
            adresse: fournisseurModel.Adresse,
            telephone: fournisseurModel.Telephone,
            email: fournisseurModel.Email
        );
    }
}