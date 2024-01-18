using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.LigneCommandeClient;

public class LigneCommandeClientMapper : ILigneCommandeClientMapper
{
    public Entities.LigneCommandeClient Mapper(LigneCommandeClientModel model, Entities.Produit produit)
    {
        return new Entities.LigneCommandeClient(
            model.Id,
            model.Quantite,
            produit
        );
    }
}