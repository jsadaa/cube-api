using ApiCube.Application.Exceptions;
using ApiCube.Domain.Enums.Administration;
using ApiCube.Domain.Mappers.Client;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Client;

public class ClientRepository : IClientRepository
{
    private readonly ApiDbContext _context;
    private readonly IClientMapper _clientMapper;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUserModel> _userManager;

    public ClientRepository(ApiDbContext context, IClientMapper clientMapper, IMapper mapper,
        UserManager<ApplicationUserModel> userManager)
    {
        _context = context;
        _clientMapper = clientMapper;
        _mapper = mapper;
        _userManager = userManager;
    }

    public void Ajouter(Domain.Entities.Client nouveauClient, string applicationUserId)
    {
        var nouveauClientModel = _mapper.Map<ClientModel>(nouveauClient);
        nouveauClientModel.ApplicationUserId = applicationUserId;

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
        var client = _context.Clients.AsNoTracking().FirstOrDefault(client => client.Id == id);
        if (client == null)
        {
            throw new ClientIntrouvable();
        }

        return _clientMapper.Mapper(client);
    }

    public void Modifier(Domain.Entities.Client client, string applicationUserId)
    {
        var clientModifié = _mapper.Map<ClientModel>(client);
        clientModifié.ApplicationUserId = applicationUserId;
        
        _context.Clients.Update(clientModifié);
        _context.SaveChanges();
    }
}