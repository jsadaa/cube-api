using ApiCube.Domain.Entities;
using ApiCube.DTOs.Requests;

namespace ApiCube.Domain.Factories;

public class FamilleProduitFactory
{
    public FamilleProduit CreerFamilleProduit(AjouterFamilleProduitRequest familleProduitRequest)
    {
        return new FamilleProduit(
            id: 0,
            nom: familleProduitRequest.Nom,
            description: familleProduitRequest.Description
        );
    }
}