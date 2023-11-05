using AutoMapper;

namespace ApiCube.Configurations;

public class StockMapperConfig : Profile
{
    public StockMapperConfig()
    {
        CreateMap<Domain.Entities.Stock, Persistence.Models.StockModel>()
            .ForMember(dest => dest.ProduitId, opt => opt.MapFrom(src => src.Produit.Id))
            .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Statut.ToString()))
            .ForMember(dest => dest.TransactionsStock, opt => opt.MapFrom(src => src.Transactions));
    }
}