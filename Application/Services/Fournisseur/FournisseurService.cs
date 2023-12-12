using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Exceptions;
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

    public BaseResponse AjouterUnFournisseur(FournisseurRequest fournisseurRequest)
    {
        try
        {
            var nouveauFournisseur = new Domain.Entities.Fournisseur(
                fournisseurRequest.Nom,
                fournisseurRequest.Adresse,
                fournisseurRequest.CodePostal,
                fournisseurRequest.Ville,
                fournisseurRequest.Pays,
                fournisseurRequest.Telephone,
                fournisseurRequest.Email
            );

            _fournisseurRepository.Ajouter(nouveauFournisseur);

            var response = new BaseResponse(
                HttpStatusCode.Created,
                new { code = "fournisseur_ajoute" }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = "fournisseur_existe_deja" }
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

    public BaseResponse ListerLesFournisseurs()
    {
        try
        {
            var listeFournisseurs = _fournisseurRepository.Lister();
            var fournisseurs = _mapper.Map<List<FournisseurResponse>>(listeFournisseurs);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                fournisseurs
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

    public BaseResponse TrouverUnFournisseur(int id)
    {
        try
        {
            var fournisseur = _fournisseurRepository.Trouver(id);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                _mapper.Map<FournisseurResponse>(fournisseur)
            );

            return response;
        }
        catch (FournisseurIntrouvable e)
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

    public BaseResponse ModifierUnFournisseur(int id, FournisseurRequest fournisseurRequest)
    {
        try
        {
            var fournisseur = _fournisseurRepository.Trouver(id);

            fournisseur.MettreAJour(
                fournisseurRequest.Nom,
                fournisseurRequest.Adresse,
                fournisseurRequest.CodePostal,
                fournisseurRequest.Ville,
                fournisseurRequest.Pays,
                fournisseurRequest.Telephone,
                fournisseurRequest.Email
            );

            _fournisseurRepository.Modifier(fournisseur);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "fournisseur_modifie" }
            );

            return response;
        }
        catch (FournisseurIntrouvable e)
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
                new { code = "fournisseur_existe_deja" }
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

    public BaseResponse SupprimerUnFournisseur(int id)
    {
        try
        {
            var fournisseur = _fournisseurRepository.Trouver(id);

            _fournisseurRepository.Supprimer(fournisseur);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "fournisseur_supprime" }
            );

            return response;
        }
        catch (FournisseurIntrouvable e)
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