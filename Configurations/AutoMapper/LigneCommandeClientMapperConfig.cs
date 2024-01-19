using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class LigneCommandeClientMapperConfig : Profile
{
    public LigneCommandeClientMapperConfig()
    {
        CreateMap<LigneCommandeClient, LigneCommandeClientResponse>()
            .ForMember(dest => dest.Quantite, opt => opt.MapFrom(src => src.Quantite))
            .ForMember(dest => dest.PrixUnitaire, opt => opt.MapFrom(src => src.PrixUnitaire))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Produit, opt => opt.MapFrom(src => src.Produit));

        CreateMap<LigneCommandeClient, LigneCommandeClientModel>()
            .ForMember(dest => dest.Quantite, opt => opt.MapFrom(src => src.Quantite))
            .ForMember(dest => dest.PrixUnitaire, opt => opt.MapFrom(src => src.PrixUnitaire))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Produit, opt => opt.MapFrom(src => src.Produit));
    }
}