using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Employe;

public interface IEmployeMapper
{
    public Entities.Employe Mapper(EmployeModel employeModel);
}