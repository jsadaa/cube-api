using ApiCube.Domain.Enums.Commande;
using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.CommandeClient;

public class CommandeClientMapper : ICommandeClientMapper
{
    public Entities.CommandeClient Mapper(CommandeClientModel model, Entities.Client client, StatutCommande statut,
        List<Entities.LigneCommandeClient> ligneCommandeClient)
    {
        return new Entities.CommandeClient(
            model.Id,
            model.DateCommande,
            model.DateLivraison,
            statut,
            client
        );
    }
}