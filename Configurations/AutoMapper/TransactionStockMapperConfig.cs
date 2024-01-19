using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Stock;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class TransactionStockMapperConfig : Profile
{
    public TransactionStockMapperConfig()
    {
        CreateMap<TransactionStock, TransactionStockModel>()
            .ForMember(dest => dest.StockId, opt => opt.MapFrom(src => src.Stock.Id))
            .ForMember(dest => dest.Stock, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        CreateMap<TransactionStock, TransactionStockResponse>()
            .ForMember(dest => dest.StockId, opt => opt.MapFrom(src => src.Stock.Id))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        CreateMap<TransactionStockModel, TransactionStockResponse>();
        CreateMap<TransactionStockRequest, TransactionStockModel>();

        CreateMap<TransactionStockModel, TransactionStock>()
            .ForMember(dest => dest.Stock, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse(typeof(TypeTransactionStock), src.Type)))
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}