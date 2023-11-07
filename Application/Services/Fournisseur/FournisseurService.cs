using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Repositories.Fournisseur;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.Fournisseur;

public class FournisseurService : IFournisseurService
{
    private readonly IFournisseurRepository _fournisseurRepository;
    private readonly IMapper _mapper;

    public FournisseurService(IFournisseurRepository fournisseurRepository, IMapper mapper)
    {
        _fournisseurRepository = fournisseurRepository;
        _mapper = mapper;
    }

    public BaseResponse AjouterUnFournisseur(FournisseurRequestDTO fournisseurRequestDTO)
    {
        try
        {
            var nouveauFournisseur = new Domain.Entities.Fournisseur(
                nom: fournisseurRequestDTO.Nom,
                adresse: fournisseurRequestDTO.Adresse,
                codePostal: fournisseurRequestDTO.CodePostal,
                ville: fournisseurRequestDTO.Ville,
                pays: fournisseurRequestDTO.Pays,
                telephone: fournisseurRequestDTO.Telephone,
                email: fournisseurRequestDTO.Email
            );

            _fournisseurRepository.Ajouter(nouveauFournisseur);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Fournisseur ajouté avec succès" }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Conflict,
                data: new { message = "Ce fournisseur existe déjà" }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse ListerLesFournisseurs()
    {
        try
        {
            var listeFournisseurs = _fournisseurRepository.Lister();
            var fournisseurs = _mapper.Map<List<FournisseurResponseDTO>>(listeFournisseurs);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: fournisseurs
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );

            return response;
        }
    }
}