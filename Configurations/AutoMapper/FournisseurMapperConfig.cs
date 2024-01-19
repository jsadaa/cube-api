using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Domain.ValuesObjects;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class FournisseurMapperConfig : Profile
{
    public FournisseurMapperConfig()
    {
        CreateMap<Fournisseur, FournisseurModel>()
            .ForMember(dest => dest.Adresse, opt => opt.MapFrom(src => src.Adresse.Rue))
            .ForMember(dest => dest.CodePostal, opt => opt.MapFrom(src => src.Adresse.CodePostal))
            .ForMember(dest => dest.Ville, opt => opt.MapFrom(src => src.Adresse.Ville))
            .ForMember(dest => dest.Pays, opt => opt.MapFrom(src => src.Adresse.Pays))
            .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone.ToString()));
        CreateMap<Fournisseur, FournisseurResponse>()
            .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone.ToString()));
        CreateMap<FournisseurModel, FournisseurResponse>();
        CreateMap<FournisseurRequest, FournisseurModel>();

        CreateMap<FournisseurModel, Fournisseur>()
            .ForMember(dest => dest.Adresse,
                opt => opt.MapFrom(src => new Adresse(src.Adresse, src.CodePostal, src.Ville, src.Pays)))
            .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => new Telephone(src.Telephone)))
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}