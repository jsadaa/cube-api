using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Repositories.FactureClient;
using AutoMapper;

namespace ApiCube.Application.Services.FactureClient;

public class FactureClientService : IFactureClientService
{
    private readonly IFactureClientRepository _factureClientRepository;
    private readonly IMapper _mapper;

    public FactureClientService(IFactureClientRepository factureClientRepository, IMapper mapper)
    {
        _factureClientRepository = factureClientRepository;
        _mapper = mapper;
    }

    public BaseResponse TrouverUneFacture(int id)
    {
        try
        {
            var facture = _factureClientRepository.Trouver(id);
            var factureResponse = _mapper.Map<FactureClientResponse>(facture);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                factureResponse
            );

            return response;
        }
        catch (FactureClientIntrouvable e)
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

    public BaseResponse TrouverUneFactureParCommande(int id)
    {
        try
        {
            var facture = _factureClientRepository.TrouverParCommande(id);
            var factureResponse = _mapper.Map<FactureClientResponse>(facture);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                factureResponse
            );

            return response;
        }
        catch (FactureClientIntrouvable e)
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

    public BaseResponse ListerLesFactures()
    {
        try
        {
            var listeFactures = _factureClientRepository.Lister();
            var listeFacturesResponse = _mapper.Map<List<FactureClientResponse>>(listeFactures);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                listeFacturesResponse
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

    public BaseResponse ListerLesFacturesDUnClient(int id)
    {
        try
        {
            var listeFactures = _factureClientRepository.ListerParClient(id);
            var listeFacturesResponse = _mapper.Map<List<FactureClientResponse>>(listeFactures);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                listeFacturesResponse
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

    public BaseResponse MarquerUneFactureCommePayee(int id)
    {
        try
        {
            var facture = _factureClientRepository.Trouver(id);
            facture.Payer();
            _factureClientRepository.Modifier(facture);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                "facture_marquee_comme_payee"
            );

            return response;
        }
        catch (FactureClientIntrouvable e)
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