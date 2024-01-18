using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Employe;

public class EmployeMapper : IEmployeMapper
{
    public Entities.Employe Mapper(EmployeModel employeModel)
    {
        return new Entities.Employe(
            employeModel.Id,
            employeModel.Nom,
            employeModel.Prenom,
            employeModel.Email,
            employeModel.DateEmbauche,
            employeModel.DateDepart,
            employeModel.Poste,
            employeModel.ApplicationUserId
        );
    }
}