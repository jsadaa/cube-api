using ApiCube.Application.DTOs.Requests;
using ApiCube.Domain.Entities;

namespace ApiCube.Domain.Factories;

public class FournisseurFactory
{
    
    public Fournisseur CreerFournisseur(AjouterFournisseurRequest fournisseurRequest)
    {
        return new Fournisseur(
            id: 0,
            nom: fournisseurRequest.Nom,
            adresse: fournisseurRequest.Adresse,
            telephone: fournisseurRequest.Telephone,
            email: fournisseurRequest.Email
        );
    }
}