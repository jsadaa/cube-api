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

    public BaseResponse AjouterUneFamilleProduit(FamilleProduitRequest familleProduitRequest)
    {
        try
        {
            var nouvelleFamilleProduit = new Domain.Entities.FamilleProduit(
                familleProduitRequest.Nom,
                familleProduitRequest.Description
            );

            _familleProduitRepository.Ajouter(nouvelleFamilleProduit);

            var response = new BaseResponse(
                HttpStatusCode.Created,
                new { code = "famille_produit_ajoute" }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = "famille_produit_existe_deja" }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse ListerLesFamillesProduits()
    {
        try
        {
            var listeFamillesProduits = _familleProduitRepository.Lister();
            var famillesProduits = _mapper.Map<List<FamilleProduitResponse>>(listeFamillesProduits);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                famillesProduits
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse TrouverUneFamilleProduit(int id)
    {
        try
        {
            var familleProduit = _familleProduitRepository.Trouver(id);
            var familleProduitResponse = _mapper.Map<FamilleProduitResponse>(familleProduit);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                familleProduitResponse
            );

            return response;
        }
        catch (FamilleProduitIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse ModifierUneFamilleProduit(int id, FamilleProduitRequest familleProduitRequest)
    {
        try
        {
            var familleProduitAModifier = _familleProduitRepository.Trouver(id);

            familleProduitAModifier.MettreAJour(
                familleProduitRequest.Nom,
                familleProduitRequest.Description
            );

            _familleProduitRepository.Modifier(familleProduitAModifier);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "famille_produit_modifiee" }
            );

            return response;
        }
        catch (FamilleProduitIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = "famille_produit_existe_deja" }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse SupprimerUneFamilleProduit(int id)
    {
        try
        {
            var familleProduitASupprimer = _familleProduitRepository.Trouver(id);

            _familleProduitRepository.Supprimer(familleProduitASupprimer);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "famille_produit_supprimee" }
            );

            return response;
        }
        catch (FamilleProduitIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }
}