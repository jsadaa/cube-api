using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class PromotionMapperConfig : Profile
{
    public PromotionMapperConfig()
    {
        CreateMap<Promotion, PromotionModel>();
        CreateMap<Promotion, PromotionResponse>();
        CreateMap<PromotionModel, PromotionResponse>();
        CreateMap<PromotionRequest, PromotionModel>();
        CreateMap<PromotionModel, Promotion>();
    }
}