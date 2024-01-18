using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.PanierClient;

public class PanierClientMapper : IPanierClientMapper
{
    public Entities.PanierClient Mapper(PanierClientModel panierClientModel, Entities.Client client,
        List<Entities.LignePanierClient> lignePanierClients)
    {
        return new Entities.PanierClient(
            panierClientModel.Id,
            client,
            lignePanierClients
        );
    }
}