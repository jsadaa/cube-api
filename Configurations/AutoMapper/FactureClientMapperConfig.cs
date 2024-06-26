using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class FactureClientMapperConfig : Profile
{
    public FactureClientMapperConfig()
    {
        CreateMap<FactureClient, FactureClientResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DateFacture, opt => opt.MapFrom(src => src.DateFacture))
            .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Statut.ToString()))
            .ForMember(dest => dest.CommandeClient, opt => opt.MapFrom(src => src.CommandeClient))
            .ForMember(dest => dest.PrixHt, opt => opt.MapFrom(src => src.PrixHt))
            .ForMember(dest => dest.PrixTtc, opt => opt.MapFrom(src => src.PrixTtc))
            .ForMember(dest => dest.Tva, opt => opt.MapFrom(src => src.Tva));

        CreateMap<FactureClient, FactureClientModel>()
            .ForMember(dest => dest.DateFacture, opt => opt.MapFrom(src => src.DateFacture))
            .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Statut.ToString()))
            .ForMember(dest => dest.CommandeClient, opt => opt.MapFrom(src => src.CommandeClient))
            .ForMember(dest => dest.PrixHt, opt => opt.MapFrom(src => src.PrixHt))
            .ForMember(dest => dest.PrixTtc, opt => opt.MapFrom(src => src.PrixTtc))
            .ForMember(dest => dest.Tva, opt => opt.MapFrom(src => src.Tva));
    }
}