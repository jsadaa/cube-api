using ApiCube.Domain.Enums.Facture;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.FactureClient;

public class FactureClientMapper : IFactureClientMapper
{
    public Entities.FactureClient Mapper(FactureClientModel model, Entities.Client client,
        Entities.CommandeClient commandeClient, StatutFacture statutFacture)
    {
        return new Entities.FactureClient(
            model.Id,
            model.DateFacture,
            statutFacture,
            model.Tva,
            client,
            commandeClient
        );
    }
}