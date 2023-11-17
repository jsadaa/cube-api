using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class EmployeMapperConfig : Profile
{
    public EmployeMapperConfig()
    {
        CreateMap<Employe, EmployeModel>();
    }
}