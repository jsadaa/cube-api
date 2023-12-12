using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class CommandeFournisseurMapperConfig : Profile
{
    public CommandeFournisseurMapperConfig()
    {
        CreateMap<CommandeFournisseur, CommandeFournisseurModel>()
            .ForMember(dest => dest.FournisseurId, opt => opt.MapFrom(src => src.Fournisseur.Id))
            .ForMember(dest => dest.Fournisseur, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeId, opt => opt.MapFrom(src => src.Employe.Id))
            .ForMember(dest => dest.Employe, opt => opt.Ignore())
            .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Statut.ToString()))
            .ForMember(dest => dest.LigneCommandeFournisseurs,
                opt => opt.MapFrom(src => src.LigneCommandeFournisseurs));
        CreateMap<CommandeFournisseur, CommandeFournisseurResponse>()
            .ForMember(dest => dest.Fournisseur, opt => opt.MapFrom(src => src.Fournisseur))
            .ForMember(dest => dest.Employe, opt => opt.MapFrom(src => src.Employe))
            .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Statut.ToString()));
    }
}