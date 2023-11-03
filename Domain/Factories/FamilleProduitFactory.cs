using ApiCube.Application.DTOs.Requests;
using ApiCube.Domain.Entities;

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