using ApiCube.Persistence.Models;
using AutoMapper;

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

    public void Ajouter(Domain.Entities.Employe employe, string userId)
    {
        var employeModel = _mapper.Map<Domain.Entities.Employe, EmployeModel>(employe);
        employeModel.ApplicationUserId = userId;

        _context.Employes.Add(employeModel);
        _context.SaveChanges();
    }
}