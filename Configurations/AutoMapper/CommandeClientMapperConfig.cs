using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Persistence.Models;
using AutoMapper;

namespace ApiCube.Configurations.AutoMapper;

public class CommandeClientMapperConfig : Profile
{
    public CommandeClientMapperConfig()
    {
        CreateMap<CommandeClient, CommandeClientResponse>()
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
            .ForMember(dest => dest.LigneCommandeClients, opt => opt.MapFrom(src => src.LigneCommandeClients))
            .ForMember(dest => dest.DateCommande, opt => opt.MapFrom(src => src.DateCommande))
            .ForMember(dest => dest.DateLivraison, opt => opt.MapFrom(src => src.DateLivraison))
            .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Statut.ToString()))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Uuid, opt => opt.MapFrom(src => src.Uuid));


        CreateMap<CommandeClient, CommandeClientModel>()
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
            .ForMember(dest => dest.LigneCommandeClients, opt => opt.MapFrom(src => src.LigneCommandeClients))
            .ForMember(dest => dest.DateCommande, opt => opt.MapFrom(src => src.DateCommande))
            .ForMember(dest => dest.DateLivraison, opt => opt.MapFrom(src => src.DateLivraison))
            .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Statut))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Uuid, opt => opt.MapFrom(src => src.Uuid));

        CreateMap<LigneCommandeClient, LigneCommandeClientResponse>()
            .ForMember(dest => dest.Produit, opt => opt.MapFrom(src => src.Produit))
            .ForMember(dest => dest.PrixUnitaire, opt => opt.MapFrom(src => src.Produit.PrixVente))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Produit.PrixVente * src.Quantite))
            .ForMember(dest => dest.Quantite, opt => opt.MapFrom(src => src.Quantite))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<LigneCommandeClient, LigneCommandeClientModel>()
            .ForMember(dest => dest.Produit, opt => opt.MapFrom(src => src.Produit))
            .ForMember(dest => dest.PrixUnitaire, opt => opt.MapFrom(src => src.Produit.PrixVente))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Produit.PrixVente * src.Quantite))
            .ForMember(dest => dest.Quantite, opt => opt.MapFrom(src => src.Quantite));
    }
}