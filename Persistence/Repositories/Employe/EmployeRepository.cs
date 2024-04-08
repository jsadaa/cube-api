using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Employe;

public class EmployeRepository : IEmployeRepository
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public EmployeRepository(ApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Ajouter(Domain.Entities.Employe employe)
    {
        var employeModel = _mapper.Map<Domain.Entities.Employe, EmployeModel>(employe);
        _context.Employes.Add(employeModel);
        _context.SaveChanges();
    }

    public List<Domain.Entities.Employe> Lister()
    {
        var employes = _context.Employes.AsNoTracking().ToList();
        return employes.Select(employe => _mapper.Map<Domain.Entities.Employe>(employe)).ToList();
    }

    public Domain.Entities.Employe Trouver(int id)
    {
        var employe = _context.Employes.AsNoTracking().FirstOrDefault(e => e.Id == id);
        if (employe == null) throw new EmployeIntrouvable();
        return _mapper.Map<Domain.Entities.Employe>(employe);
    }

    public Domain.Entities.Employe TrouverParApplicationUserId(string applicationUserId)
    {
        var employe = _context.Employes.AsNoTracking().FirstOrDefault(e => e.ApplicationUserId == applicationUserId);
        if (employe == null) throw new EmployeIntrouvable();
        return _mapper.Map<Domain.Entities.Employe>(employe);
    }

    public void Modifier(Domain.Entities.Employe employe)
    {
        var employeModifie = _mapper.Map<EmployeModel>(employe);
        _context.Employes.Update(employeModifie);
        _context.SaveChangesAsync();
    }
}