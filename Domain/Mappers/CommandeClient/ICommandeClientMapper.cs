using ApiCube.Domain.Enums.Commande;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.CommandeClient;

public interface ICommandeClientMapper
{
    public Entities.CommandeClient Mapper(CommandeClientModel model, Entities.Client client, StatutCommande statut,
        List<Entities.LigneCommandeClient> ligneCommandeClient);
}