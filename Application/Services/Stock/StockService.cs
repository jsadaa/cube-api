using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Requests.Stock;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Stock;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.Stock;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.Stock;

public class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;
    private readonly IProduitRepository _produitRepository;
    private readonly IMapper _mapper;

    public StockService(
        IStockRepository stockRepository,
        IProduitRepository produitRepository,
        IMapper mapper
    )
    {
        _stockRepository = stockRepository;
        _produitRepository = produitRepository;
        _mapper = mapper;
    }

    public BaseResponse AjouterUnStockDeProduit(StockRequest stockRequest)
    {
        try
        {
            var produit = _produitRepository.Trouver(stockRequest.ProduitId);

            // on met la quantité à 0 car elle sera mise à jour lors de la première transaction
            var nouveauStock = new Domain.Entities.Stock(
                quantite: 0,
                seuilDisponibilite: stockRequest.SeuilDisponibilite,
                produit: produit,
                transactionStocks: new List<TransactionStock>(),
                dateCreation: DateTime.Now,
                datePeremption: stockRequest.DatePeremption,
                dateModification: DateTime.Now,
                dateSuppression: null
            );

            // Ajout d'une transaction stock pour l'achat initial
            var nouvelleTransactionStock = new TransactionStock(
                quantite: stockRequest.Quantite,
                date: DateTime.Now,
                type: TypeTransactionStock.Achat,
                stock: nouveauStock,
                prixUnitaire: produit.PrixAchat,
                quantiteAvant: 0,
                quantiteApres: stockRequest.Quantite
            );

            nouveauStock.AjouterTransaction(nouvelleTransactionStock);
            _stockRepository.Ajouter(nouveauStock);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Stock ajouté avec succès" }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Conflict,
                data: new { message = "Ce stock existe déjà" }
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

    public BaseResponse ListerLesStocks()
    {
        try
        {
            var listeStocks = _stockRepository.Lister();
            var stocks = _mapper.Map<List<StockResponse>>(listeStocks);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: stocks
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

    public BaseResponse TrouverUnStock(int id)
    {
        try
        {
            var stock = _stockRepository.Trouver(id);
            var stockResponse = _mapper.Map<StockResponse>(stock);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: stockResponse
            );

            return response;
        }
        catch (StockIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = e.Message }
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

    public BaseResponse ModifierUnStock(int id, StockUpdate stockUpdate)
    {
        try
        {
            var stock = _stockRepository.Trouver(id);

            if (stock.Quantite != stockUpdate.Quantite)
            {
                var nouvelleTransactionStock = new TransactionStock(
                    quantite: stockUpdate.Quantite - stock.Quantite,
                    date: DateTime.Now,
                    type: TypeTransactionStock.ModificationInterne,
                    stock: stock,
                    prixUnitaire: stock.Produit.PrixAchat,
                    quantiteAvant: stock.Quantite,
                    quantiteApres: stockUpdate.Quantite
                );

                stock.AjouterTransaction(nouvelleTransactionStock);
            }

            stock.ModifierSeuilDisponibilite(stockUpdate.SeuilDisponibilite);
            stock.ModifierDatePeremption(stockUpdate.DatePeremption);

            _stockRepository.Modifier(stock);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { message = "Stock modifié avec succès" }
            );

            return response;
        }
        catch (StockIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = e.Message }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Conflict,
                data: new { message = "Ce stock existe déjà" }
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

    public BaseResponse SupprimerUnStock(int id)
    {
        try
        {
            var stock = _stockRepository.Trouver(id);
            
            var transactionStock = new TransactionStock(
                quantite: - stock.Quantite,
                date: DateTime.Now,
                type: TypeTransactionStock.Suppression,
                stock: stock,
                prixUnitaire: stock.Produit.PrixAchat,
                quantiteAvant: stock.Quantite,
                quantiteApres: 0
            );
            
            stock.AjouterTransaction(transactionStock);
            
            _stockRepository.Modifier(stock);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { message = "Stock supprimé avec succès" }
            );

            return response;
        }
        catch (StockIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = e.Message }
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