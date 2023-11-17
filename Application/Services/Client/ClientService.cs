using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Exceptions;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Repositories.Client;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.Client;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse> AjouterUnClient(ClientRequest clientRequest)
    {
        try
        {
            var client = new Domain.Entities.Client(
                username: clientRequest.Nom + clientRequest.Prenom,
                nom: clientRequest.Nom,
                prenom: clientRequest.Prenom,
                adresse: clientRequest.Adresse,
                codePostal: clientRequest.CodePostal,
                ville: clientRequest.Ville,
                pays: clientRequest.Pays,
                telephone: clientRequest.Telephone,
                email: clientRequest.Email,
                dateNaissance: clientRequest.DateNaissance,
                dateInscription: DateTime.Now
            );

            await _clientRepository.Ajouter(client, clientRequest.Password);

            var response = new BaseResponse(
                HttpStatusCode.Created,
                new { code = "client_ajoute" }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = "client_existe_deja" }
            );

            return response;
        }
        catch (UtilisateurExisteDeja e)
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = e.Message }
            );

            return response;
        }
        catch (FormatMotDePasseInvalide e)
        {
            var response = new BaseResponse(
                HttpStatusCode.BadRequest,
                new { code = e.Message }
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

    public BaseResponse ListerLesClients()
    {
        try
        {
            var listeClients = _clientRepository.Lister();
            var clients = _mapper.Map<List<ClientResponse>>(listeClients);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                clients
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

    public BaseResponse TrouverUnClient(int id)
    {
        try
        {
            var client = _clientRepository.Trouver(id);
            var clientResponse = _mapper.Map<ClientResponse>(client);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                clientResponse
            );

            return response;
        }
        catch (ClientIntrouvable e)
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

    public async Task<BaseResponse> ModifierUnClient(int id, ClientRequest clientRequest)
    {
        try
        {
            var client = _clientRepository.Trouver(id);

            client.MettreAJour(
                nom: clientRequest.Nom,
                prenom: clientRequest.Prenom,
                adresse: clientRequest.Adresse,
                codePostal: clientRequest.CodePostal,
                ville: clientRequest.Ville,
                pays: clientRequest.Pays,
                telephone: clientRequest.Telephone,
                email: clientRequest.Email,
                dateNaissance: clientRequest.DateNaissance
            );

            await _clientRepository.Modifier(client, clientRequest.Password);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "client_modifie" }
            );

            return response;
        }
        catch (ClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (UtilisateurIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (FormatMotDePasseInvalide e)
        {
            var response = new BaseResponse(
                HttpStatusCode.BadRequest,
                new { code = e.Message }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = "client_existe_deja" }
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

    public async Task<BaseResponse> SupprimerUnClient(int id)
    {
        try
        {
            var client = _clientRepository.Trouver(id);

            await _clientRepository.Supprimer(client);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "client_supprime" }
            );

            return response;
        }
        catch (ClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (UtilisateurIntrouvable e)
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