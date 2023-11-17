using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Client;

public class ClientMapper : IClientMapper
{
    public Entities.Client Mapper(ClientModel clientModel)
    {
        return new Entities.Client(
            id: clientModel.Id,
            username: clientModel.Nom + clientModel.Prenom,
            nom: clientModel.Nom,
            prenom: clientModel.Prenom,
            adresse: clientModel.Adresse,
            codePostal: clientModel.CodePostal,
            ville: clientModel.Ville,
            pays: clientModel.Pays,
            telephone: clientModel.Telephone,
            email: clientModel.Email,
            dateNaissance: clientModel.DateNaissance,
            dateInscription: clientModel.DateInscription
        );
    }
}