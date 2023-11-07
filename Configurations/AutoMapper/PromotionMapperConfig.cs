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
        CreateMap<Promotion, PromotionResponseDTO>();
        CreateMap<PromotionModel, PromotionResponseDTO>();
        CreateMap<PromotionRequestDTO, PromotionModel>();
    }
}