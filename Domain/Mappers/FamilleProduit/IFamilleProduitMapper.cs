using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.FamilleProduit;

public interface IFamilleProduitMapper
{
    public Entities.FamilleProduit Mapper(FamilleProduitModel familleProduitModel);
}