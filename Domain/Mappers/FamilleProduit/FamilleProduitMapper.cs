using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.FamilleProduit;

public class FamilleProduitMapper : IFamilleProduitMapper
{
    public Entities.FamilleProduit Mapper(FamilleProduitModel familleProduitModel)
    {
        return new Entities.FamilleProduit(
            id: familleProduitModel.Id,
            nom: familleProduitModel.Nom,
            description: familleProduitModel.Description
        );
    }
}