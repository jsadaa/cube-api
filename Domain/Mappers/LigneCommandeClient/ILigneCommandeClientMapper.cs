using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.LigneCommandeClient;

public interface ILigneCommandeClientMapper
{
    public Entities.LigneCommandeClient Mapper(LigneCommandeClientModel model, Entities.Produit produit);
}