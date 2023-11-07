using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class FamilleProduitMapperConfig : Profile
{
    public FamilleProduitMapperConfig()
    {
        CreateMap<FamilleProduit, FamilleProduitModel>();
        CreateMap<FamilleProduit, FamilleProduitResponseDTO>();
        CreateMap<FamilleProduitModel, FamilleProduitResponseDTO>();
    }
}