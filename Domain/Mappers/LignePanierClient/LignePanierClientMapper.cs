using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.LignePanierClient;

public class LignePanierClientMapper : ILignePanierClientMapper
{
    public Entities.LignePanierClient Mapper(LignePanierClientModel lignePanierClientModel, Entities.Produit produit)
    {
        return new Entities.LignePanierClient(
            lignePanierClientModel.Id,
            produit,
            lignePanierClientModel.Quantite
        );
    }
}