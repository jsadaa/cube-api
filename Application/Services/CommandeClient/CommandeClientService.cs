using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Exceptions;
using ApiCube.Domain.Services;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Repositories.Client;
using ApiCube.Persistence.Repositories.PanierClient;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.Stock;
using AutoMapper;

namespace ApiCube.Application.Services.CommandeClient;

public class CommandeClientService : ICommandeClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly IPanierClientRepository _panierClientRepository;
    private readonly PreparateurDeStock _preparateurDeStock;
    private readonly IProduitRepository _produitRepository;
    private readonly IStockRepository _stockRepository;

    public CommandeClientService(IPanierClientRepository panierClientRepository, IProduitRepository produitRepository,
        IClientRepository clientRepository, IStockRepository stockRepository, IMapper mapper,
        PreparateurDeStock preparateurDeStock)
    {
        _panierClientRepository = panierClientRepository;
        _produitRepository = produitRepository;
        _clientRepository = clientRepository;
        _stockRepository = stockRepository;
        _mapper = mapper;
        _preparateurDeStock = preparateurDeStock;
    }

    public BaseResponse CreerUnPanier(int idClient)
    {
        try
        {
            var client = _clientRepository.Trouver(idClient);
            var panierClient = new PanierClient(client);

            _panierClientRepository.Ajouter(panierClient);

            return new BaseResponse(
                HttpStatusCode.Created,
                new { code = "panier_cree" }
            );
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

    public BaseResponse AjouterUnProduitAuPanier(int id, ProduitPanierRequest produitPanierRequest)
    {
        try
        {
            var produit = _produitRepository.Trouver(produitPanierRequest.IdProduit);
            var panierClient = _panierClientRepository.Trouver(id);
            var lignePanierClient = new LignePanierClient(produit, produitPanierRequest.Quantite);

            panierClient.AjouterLignePanierClient(lignePanierClient);

            _panierClientRepository.Ajouter(panierClient);

            return new BaseResponse(
                HttpStatusCode.Created,
                new { code = "produit_ajoute_au_panier" }
            );
        }
        catch (ProduitIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
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
        catch (QuantitePanierInvalide e)
        {
            var response = new BaseResponse(
                HttpStatusCode.BadRequest,
                new { code = e.Message }
            );

            return response;
        }
        catch (ProduitDejaDansPanier e)
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
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
    
    public BaseResponse TrouverUnPanier(int id)
    {
        try
        {
            var panierClient = _panierClientRepository.Trouver(id);
            var panierClientResponse = _mapper.Map<PanierClientResponse>(panierClient);

            return new BaseResponse(
                HttpStatusCode.OK,
                panierClientResponse
            );
        }
        catch (PanierClientIntrouvable e)
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
    
    public BaseResponse ListerLesPaniersDUnClient(int idClient)
    {
        try
        {
            var paniersClient = _panierClientRepository.ListerParClient(idClient);
            var paniersClientResponse = _mapper.Map<List<PanierClientResponse>>(paniersClient);

            return new BaseResponse(
                HttpStatusCode.OK,
                paniersClientResponse
            );
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