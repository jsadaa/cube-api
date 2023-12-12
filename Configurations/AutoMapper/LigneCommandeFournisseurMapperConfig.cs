using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class LigneCommandeFournisseurMapperConfig : Profile
{
    public LigneCommandeFournisseurMapperConfig()
    {
        CreateMap<LigneCommandeFournisseur, LigneCommandeFournisseurModel>()
            .ForMember(dest => dest.ProduitId, opt => opt.MapFrom(src => src.Produit.Id))
            .ForMember(dest => dest.Produit, opt => opt.Ignore())
            .ForMember(dest => dest.CommandeFournisseurId, opt => opt.MapFrom(src => src.CommandeFournisseurId))
            .ForMember(dest => dest.CommandeFournisseur, opt => opt.Ignore());
        CreateMap<LigneCommandeFournisseur, LigneCommandeFournisseurResponse>()
            .ForMember(dest => dest.Produit, opt => opt.MapFrom(src => src.Produit))
            .ForMember(dest => dest.CommandeFournisseurId, opt => opt.MapFrom(src => src.CommandeFournisseurId));
    }
}