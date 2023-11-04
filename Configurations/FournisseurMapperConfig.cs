using AutoMapper;

namespace ApiCube.Configurations;

public class FournisseurMapperConfig : Profile
{
    public FournisseurMapperConfig()
    {
        CreateMap<Domain.Entities.Fournisseur, Persistence.Models.FournisseurModel>()
            .ForMember(dest => dest.Adresse, opt => opt.MapFrom(src => src.Adresse.ToString()))
            .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone.ToString()));
        CreateMap<Domain.Entities.Fournisseur, Application.DTOs.Requests.FournisseurRequestDTO>()
            .ForMember(dest => dest.Adresse, opt => opt.MapFrom(src => src.Adresse.ToString()))
            .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone.ToString()));
        CreateMap<Domain.Entities.Fournisseur, Application.DTOs.Responses.FournisseurResponseDTO>()
            .ForMember(dest => dest.Adresse, opt => opt.MapFrom(src => src.Adresse.ToString()))
            .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone.ToString()));
        CreateMap<Persistence.Models.FournisseurModel, Application.DTOs.Responses.FournisseurResponseDTO>();
        CreateMap<Application.DTOs.Requests.FournisseurRequestDTO, Persistence.Models.FournisseurModel>();
    }
}