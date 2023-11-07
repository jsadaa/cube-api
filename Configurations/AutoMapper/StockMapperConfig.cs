using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class StockMapperConfig : Profile
{
    public StockMapperConfig()
    {
        CreateMap<Stock, StockModel>()
            .ForMember(dest => dest.ProduitId, opt => opt.MapFrom(src => src.Produit.Id))
            .ForMember(dest => dest.Produit, opt => opt.Ignore())
            .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Statut.ToString()))
            .ForMember(dest => dest.TransactionsStock, opt => opt.MapFrom(src => src.Transactions));
        CreateMap<Stock, StockResponseDTO>()
            .ForMember(dest => dest.Produit, opt => opt.MapFrom(src => src.Produit))
            .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Statut.ToString()));
        CreateMap<StockModel, StockResponseDTO>();
        CreateMap<StockRequestDTO, StockModel>();
    }
}