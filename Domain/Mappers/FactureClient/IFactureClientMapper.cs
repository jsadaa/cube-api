using ApiCube.Domain.Enums.Facture;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.FactureClient;

public interface IFactureClientMapper
{
    public Entities.FactureClient Mapper(FactureClientModel model,
        Entities.CommandeClient commandeClient, StatutFacture statutFacture);
}