using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.FamilleProduit;

public class FamilleProduitMapper : IFamilleProduitMapper
{
    public Entities.FamilleProduit Mapper(FamilleProduitModel familleProduitModel)
    {
        return new Entities.FamilleProduit(
            familleProduitModel.Id,
            familleProduitModel.Nom,
            familleProduitModel.Description
        );
    }
}