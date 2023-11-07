using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.ValuesObjects;
using AutoMapper;

namespace ApiCube.Configurations;

public class AdresseMapperConfig : Profile
{
    public AdresseMapperConfig()
    {
        CreateMap<Adresse, AdresseDTO>();
    }
}