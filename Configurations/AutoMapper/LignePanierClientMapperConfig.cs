using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class LignePanierClientMapperConfig : Profile
{
    public LignePanierClientMapperConfig()
    {
        CreateMap<LignePanierClient, LignePanierClientModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Produit, opt => opt.MapFrom(src => src.Produit))
            .ForMember(dest => dest.Quantite, opt => opt.MapFrom(src => src.Quantite))
            .ForMember(dest => dest.PrixUnitaire, opt => opt.MapFrom(src => src.PrixUnitaire))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

        CreateMap<LignePanierClient, LignePanierClientResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Produit, opt => opt.MapFrom(src => src.Produit))
            .ForMember(dest => dest.Quantite, opt => opt.MapFrom(src => src.Quantite))
            .ForMember(dest => dest.PrixUnitaire, opt => opt.MapFrom(src => src.PrixUnitaire))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));
    }
}