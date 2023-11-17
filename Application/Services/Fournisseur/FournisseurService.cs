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
                nom: fournisseurRequest.Nom,
                adresse: fournisseurRequest.Adresse,
                codePostal: fournisseurRequest.CodePostal,
                ville: fournisseurRequest.Ville,
                pays: fournisseurRequest.Pays,
                telephone: fournisseurRequest.Telephone,
                email: fournisseurRequest.Email
            );

            _fournisseurRepository.Ajouter(nouveauFournisseur);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { code = "fournisseur_ajoute" }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Conflict,
                data: new { code = "fournisseur_existe_deja" }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { code = "unexpected_error", message = e.Message }
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
                statusCode: HttpStatusCode.OK,
                data: fournisseurs
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { code = "unexpected_error", message = e.Message }
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
                statusCode: HttpStatusCode.OK,
                data: _mapper.Map<FournisseurResponse>(fournisseur)
            );

            return response;
        }
        catch (FournisseurIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { code = "unexpected_error", message = e.Message }
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
                nom: fournisseurRequest.Nom,
                adresse: fournisseurRequest.Adresse,
                codePostal: fournisseurRequest.CodePostal,
                ville: fournisseurRequest.Ville,
                pays: fournisseurRequest.Pays,
                telephone: fournisseurRequest.Telephone,
                email: fournisseurRequest.Email
            );

            _fournisseurRepository.Modifier(fournisseur);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { code = "fournisseur_modifie" }
            );

            return response;
        }
        catch (FournisseurIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { code = e.Message }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Conflict,
                data: new { code = "fournisseur_existe_deja" }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { code = "unexpected_error", message = e.Message }
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
                statusCode: HttpStatusCode.OK,
                data: new { code = "fournisseur_supprime" }
            );

            return response;
        }
        catch (FournisseurIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }
}