using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.LignePanierClient;

public interface ILignePanierClientMapper
{
    public Entities.LignePanierClient Mapper(LignePanierClientModel lignePanierClientModel, Entities.Produit produit);
}