using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests.Stock;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Services;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.Stock;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.Stock;

public class StockService : IStockService
{
    private readonly IMapper _mapper;
    private readonly PreparateurDeStock _preparateurDeStock;
    private readonly IProduitRepository _produitRepository;
    private readonly IStockRepository _stockRepository;

    public StockService(
        IStockRepository stockRepository,
        IProduitRepository produitRepository,
        PreparateurDeStock preparateurDeStock,
        IMapper mapper
    )
    {
        _stockRepository = stockRepository;
        _produitRepository = produitRepository;
        _preparateurDeStock = preparateurDeStock;
        _mapper = mapper;
    }

    public BaseResponse AjouterUnStockDeProduit(StockRequest stockRequest)
    {
        try
        {
            var produit = _produitRepository.Trouver(stockRequest.ProduitId);

            var nouveauStock = _preparateurDeStock.AjoutInterne(
                produit,
                stockRequest
            );

            _stockRepository.Ajouter(nouveauStock);

            var response = new BaseResponse(
                HttpStatusCode.Created,
                new { code = "stock_ajoute" }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = "stock_existe_deja" }
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

    public BaseResponse ListerLesStocks()
    {
        try
        {
            var listeStocks = _stockRepository.Lister();
            var stocks = _mapper.Map<List<StockResponse>>(listeStocks);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                stocks
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

    public BaseResponse TrouverUnStock(int id)
    {
        try
        {
            var stock = _stockRepository.Trouver(id);
            var stockResponse = _mapper.Map<StockResponse>(stock);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                stockResponse
            );

            return response;
        }
        catch (StockIntrouvable e)
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

    public BaseResponse ModifierUnStock(int id, StockUpdate stockUpdate)
    {
        try
        {
            var stock = _stockRepository.Trouver(id);

            var stockModifié = _preparateurDeStock.ModificationInterne(
                stock,
                stockUpdate
            );

            _stockRepository.Modifier(stockModifié);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "stock_modifie" }
            );

            return response;
        }
        catch (StockIntrouvable e)
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
                new { code = "stock_existe_deja" }
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

    public BaseResponse SupprimerUnStock(int id)
    {
        try
        {
            var stock = _stockRepository.Trouver(id);
            var stockSupprimé = _preparateurDeStock.Suppression(stock);

            // On applique un soft delete ici,
            // On ne supprime pas le stock de la base de données mais on le rend indisponible
            _stockRepository.Modifier(stockSupprimé);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "stock_supprime" }
            );

            return response;
        }
        catch (StockIntrouvable e)
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