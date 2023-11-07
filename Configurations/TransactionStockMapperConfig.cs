using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations;

public class TransactionStockMapperConfig : Profile
{
    public TransactionStockMapperConfig()
    {
        CreateMap<TransactionStock, TransactionStockModel>()
            .ForMember(dest => dest.StockId, opt => opt.MapFrom(src => src.Stock.Id))
            .ForMember(dest => dest.Stock, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        CreateMap<TransactionStock, TransactionStockResponseDTO>()
            .ForMember(dest => dest.StockId, opt => opt.MapFrom(src => src.Stock.Id))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        CreateMap<TransactionStockModel, TransactionStockResponseDTO>();
        CreateMap<TransactionStockRequestDTO, TransactionStockModel>();
    }
}