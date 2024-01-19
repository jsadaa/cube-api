using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Facture;

namespace ApiCube.Domain.Services;

public class GestionnaireDeFacturation
{
    public FactureClient Commande(CommandeClient commandeClient)
    {
        var factureClient = new FactureClient(
            DateTime.Now,
            StatutFacture.EnCours,
            0.2,
            commandeClient
        );

        return factureClient;
    }
}