using ApiCube.Domain.Mappers.Employe;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Employe;

public class EmployeRepository : IEmployeRepository
{
    private readonly ApiDbContext _context;
    private readonly IEmployeMapper _employeMapper;
    private readonly IMapper _mapper;

    public EmployeRepository(ApiDbContext context, IMapper mapper, IEmployeMapper employeMapper)
    {
        _context = context;
        _mapper = mapper;
        _employeMapper = employeMapper;
    }

    public void Ajouter(Domain.Entities.Employe employe, string applicationUserId)
    {
        var employeModel = _mapper.Map<Domain.Entities.Employe, EmployeModel>(employe);
        employeModel.ApplicationUserId = applicationUserId;

        _context.Employes.Add(employeModel);
        _context.SaveChanges();
    }

    public List<Domain.Entities.Employe> Lister()
    {
        var employes = _context.Employes.AsNoTracking().ToList();
        return employes.Select(employe => _employeMapper.Mapper(employe)).ToList();
    }

    public Domain.Entities.Employe Trouver(int id)
    {
        var employe = _context.Employes.AsNoTracking().FirstOrDefault(e => e.Id == id);
        if (employe == null) throw new EmployeIntrouvable();

        return _employeMapper.Mapper(employe);
    }

    public void Modifier(Domain.Entities.Employe employe, string applicationUserId)
    {
        var employeModifie = _mapper.Map<EmployeModel>(employe);
        employeModifie.ApplicationUserId = applicationUserId;

        _context.Employes.Update(employeModifie);
        _context.SaveChangesAsync();
    }
}