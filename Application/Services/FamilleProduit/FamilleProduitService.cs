using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Repositories.FamilleProduit;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.FamilleProduit;

public class FamilleProduitService : IFamilleProduitService
{
    private readonly IFamilleProduitRepository _familleProduitRepository;
    private readonly IMapper _mapper;

    public FamilleProduitService(IFamilleProduitRepository familleProduitRepository, IMapper mapper)
    {
        _familleProduitRepository = familleProduitRepository;
        _mapper = mapper;
    }

    public BaseResponse AjouterUneFamilleProduit(FamilleProduitRequestDTO familleProduitRequestDTO)
    {
        try
        {
            var nouvelleFamilleProduit = new Domain.Entities.FamilleProduit(
                nom: familleProduitRequestDTO.Nom,
                description: familleProduitRequestDTO.Description
            );

            _familleProduitRepository.Ajouter(nouvelleFamilleProduit);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Famille de produit ajoutée avec succès" }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Conflict,
                data: new { message = "Cette famille de produit existe déjà" }
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

    public BaseResponse ListerLesFamillesProduits()
    {
        try
        {
            var listeFamillesProduits = _familleProduitRepository.Lister();
            var famillesProduits = _mapper.Map<List<FamilleProduitResponseDTO>>(listeFamillesProduits);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: famillesProduits
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
    
    public BaseResponse TrouverUneFamilleProduit(int id)
    {
        try
        {
            var familleProduit = _familleProduitRepository.Trouver(id);
            var familleProduitResponse = _mapper.Map<FamilleProduitResponseDTO>(familleProduit);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: familleProduitResponse
            );

            return response;
        }
        catch (FamilleProduitIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = "Cette famille de produit n'existe pas" }
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