using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.PanierClient;

public interface IPanierClientMapper
{
    public Entities.PanierClient Mapper(PanierClientModel panierClientModel, Entities.Client client,
        List<Entities.LignePanierClient> lignePanierClients);
}