using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Client;

public interface IClientMapper
{
    public Entities.Client Mapper(ClientModel clientModel);
}