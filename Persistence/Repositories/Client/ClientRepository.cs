using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Persistence.Repositories.Client;

public class ClientRepository : IClientRepository
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public ClientRepository(ApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Ajouter(Domain.Entities.Client nouveauClient, string idApplicationUser)
    {
        var nouveauClientModel = _mapper.Map<ClientModel>(nouveauClient);
        nouveauClientModel.ApplicationUserId = idApplicationUser;

        _context.Clients.Add(nouveauClientModel);
        _context.SaveChanges();
    }
}