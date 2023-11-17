using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class ClientMapperConfig : Profile
{
    public ClientMapperConfig()
    {
        CreateMap<Client, ClientModel>()
            .ForMember(dest => dest.Adresse, opt => opt.MapFrom(src => src.Adresse.Rue))
            .ForMember(dest => dest.CodePostal, opt => opt.MapFrom(src => src.Adresse.CodePostal))
            .ForMember(dest => dest.Ville, opt => opt.MapFrom(src => src.Adresse.Ville))
            .ForMember(dest => dest.Pays, opt => opt.MapFrom(src => src.Adresse.Pays))
            .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone.ToString()));
    }
}