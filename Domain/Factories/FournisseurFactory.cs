using ApiCube.Domain.Entities;
using ApiCube.DTOs.Requests;

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