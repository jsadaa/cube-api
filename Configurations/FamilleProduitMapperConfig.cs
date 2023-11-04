using AutoMapper;

namespace ApiCube.Configurations;

public class FamilleProduitMapperConfig : Profile
{
    public FamilleProduitMapperConfig()
    {
        CreateMap<Domain.Entities.FamilleProduit, Persistence.Models.FamilleProduitModel>();
        CreateMap<Domain.Entities.FamilleProduit, Application.DTOs.Responses.FamilleProduitResponseDTO>();
        CreateMap<Persistence.Models.FamilleProduitModel, Application.DTOs.Responses.FamilleProduitResponseDTO>();
        CreateMap<Persistence.Models.FamilleProduitModel, Domain.Entities.FamilleProduit>();
        CreateMap<Application.DTOs.Requests.FamilleProduitRequestDTO, Domain.Entities.FamilleProduit>();
    }
}