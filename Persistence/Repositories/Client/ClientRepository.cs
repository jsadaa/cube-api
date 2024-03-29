using ApiCube.Domain.Mappers.Client;
using ApiCube.Domain.Mappers.PanierClient;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Client;

public class ClientRepository : IClientRepository
{
    private readonly IClientMapper _clientMapper;
    private readonly IPanierClientMapper _panierClientMapper;
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public ClientRepository(ApiDbContext context, IClientMapper clientMapper, IPanierClientMapper panierClientMapper, IMapper mapper)
    {
        _context = context;
        _clientMapper = clientMapper;
        _panierClientMapper = panierClientMapper;
        _mapper = mapper;
    }

    public void Ajouter(Domain.Entities.Client nouveauClient)
    {
        var nouveauClientModel = _mapper.Map<ClientModel>(nouveauClient);

        _context.Clients.Add(nouveauClientModel);
        _context.SaveChangesAsync();
    }

    public List<Domain.Entities.Client> Lister()
    {
        var clients = _context.Clients.AsNoTracking().ToList();
        return clients.Select(client => _clientMapper.Mapper(client)).ToList();
    }

    public Domain.Entities.Client Trouver(int id)
    {
        var client = _context.Clients
            .Include(c => c.Panier) // Inclure le panier client
            .Include(c => c.Commandes) // Inclure les commandes clients
            .Include(c => c.Factures) // Inclure les factures clients
            .AsNoTracking()
            .FirstOrDefault(client => client.Id == id);
        
        if (client == null) throw new ClientIntrouvable();
        
        return _clientMapper.Mapper(client);
    }
    
    public Domain.Entities.Client TrouverParApplicationUserId(string applicationUserId)
    {
        var client = _context.Clients.AsNoTracking().FirstOrDefault(client => client.ApplicationUserId == applicationUserId);
        if (client == null) throw new ClientIntrouvable();

        return _clientMapper.Mapper(client);
    }

    public void Modifier(Domain.Entities.Client client)
    {
        var clientModifié = _mapper.Map<ClientModel>(client);

        _context.Clients.Update(clientModifié);
        _context.SaveChanges();
    }
}