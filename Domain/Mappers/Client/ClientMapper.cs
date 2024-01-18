using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Client;

public class ClientMapper : IClientMapper
{
    public Entities.Client Mapper(ClientModel clientModel)
    {
        return new Entities.Client(
            clientModel.Id,
            clientModel.Nom + clientModel.Prenom,
            clientModel.Nom,
            clientModel.Prenom,
            clientModel.Adresse,
            clientModel.CodePostal,
            clientModel.Ville,
            clientModel.Pays,
            clientModel.Telephone,
            clientModel.Email,
            clientModel.DateNaissance,
            clientModel.DateInscription,
            clientModel.ApplicationUserId
        );
    }
}