using ApiCube.Persistence.Models;

namespace ApiCube.Domain.Mappers.Employe;

public class EmployeMapper : IEmployeMapper
{
    public Entities.Employe Mapper(EmployeModel employeModel)
    {
        return new Entities.Employe(
            id: employeModel.Id,
            nom: employeModel.Nom,
            prenom: employeModel.Prenom,
            email: employeModel.Email,
            dateEmbauche: employeModel.DateEmbauche,
            dateDepart: employeModel.DateDepart,
            statut: employeModel.Statut
        );
    }
}