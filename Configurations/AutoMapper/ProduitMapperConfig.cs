using ApiCube.Application.DTOs.Requests.Produit;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class ProduitMapperConfig : Profile
{
    public ProduitMapperConfig()
    {
        CreateMap<Produit, ProduitModel>()
            .ForMember(dest => dest.FamilleProduitId, opt => opt.MapFrom(src => src.FamilleProduit.Id))
            .ForMember(dest => dest.FamilleProduit, opt => opt.Ignore())
            .ForMember(dest => dest.FournisseurId, opt => opt.MapFrom(src => src.Fournisseur.Id))
            .ForMember(dest => dest.Fournisseur, opt => opt.Ignore())
            .ForMember(dest => dest.PromotionId,
                opt => opt.MapFrom(src => src.Promotion != null ? src.Promotion.Id : (int?)null))
            .ForMember(dest => dest.Promotion, opt => opt.Ignore());
        CreateMap<Produit, ProduitResponse>()
            .ForMember(dest => dest.FamilleProduitNom, opt => opt.MapFrom(src => src.FamilleProduit.Nom))
            .ForMember(dest => dest.FournisseurNom, opt => opt.MapFrom(src => src.Fournisseur.Nom))
            .ForMember(dest => dest.EnPromotion, opt => opt.MapFrom(src => src.EnPromotion));
        CreateMap<ProduitModel, ProduitResponse>()
            .ForMember(dest => dest.FamilleProduitNom, opt => opt.MapFrom(src => src.FamilleProduit.Nom))
            .ForMember(dest => dest.FournisseurNom, opt => opt.MapFrom(src => src.Fournisseur.Nom))
            .ForMember(dest => dest.EnPromotion, opt => opt.MapFrom(src => src.EnPromotion));
        CreateMap<ProduitRequest, ProduitModel>();

        CreateMap<ProduitModel, Produit>();
    }
}