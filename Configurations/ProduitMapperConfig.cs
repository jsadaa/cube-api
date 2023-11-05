using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations;

public class ProduitMapperConfig : Profile
{
    
    public ProduitMapperConfig()
    {
        CreateMap<Produit, ProduitModel>()
            .ForMember(dest => dest.FamilleProduitId, opt => opt.MapFrom(src => src.FamilleProduit.Id))
            .ForMember(dest => dest.FournisseurId, opt => opt.MapFrom(src => src.Fournisseur.Id))
            .ForMember(dest => dest.PromotionId,
                opt => opt.MapFrom(src => src.Promotion != null ? src.Promotion.Id : (int?)null));
    }
}