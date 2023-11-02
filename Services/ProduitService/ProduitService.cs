using System.Net;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Factories;
using ApiCube.Domain.Stock;
using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;
using ApiCube.Repositories;
using ApiCube.Repositories.Interfaces;

namespace ApiCube.Services.ProduitService;

public class ProduitService : IProduitService
{
    private readonly IProduitRepository _produitRepository;
    private readonly ITransactionStockRepository _transactionStockRepository;
    private readonly ProduitFactory _produitFactory;
    private readonly TransactionStockFactory _transactionStockFactory;
    
    public ProduitService(IProduitRepository produitRepository, ProduitFactory produitFactory, TransactionStockFactory transactionStockFactory, ITransactionStockRepository transactionStockRepository)
    {
        _produitRepository = produitRepository;
        _produitFactory = produitFactory;
        _transactionStockFactory = transactionStockFactory;
        _transactionStockRepository = transactionStockRepository;
    }

    public BaseResponse AjouterUnProduitAuStock(AjouterProduitRequest produitRequest)
    {
        try
        {
            Produit nouveauProduit = _produitFactory.CreerProduit(produitRequest);
            TransactionStock transactionStock = _transactionStockFactory.CreerTransactionStock(nouveauProduit, TypeTransactionStock.Achat);
            TransactionStockDTO transactionStockDTO = new TransactionStockDTO
            {
                Quantite = transactionStock.Quantite,
                Date = transactionStock.Date,
                Type = transactionStock.Type.ToString(),
                ProduitId = transactionStock.Produit.Id,
                PrixUnitaire = transactionStock.PrixUnitaire,
                PrixTotal = transactionStock.PrixTotal
            };
            
            _produitRepository.Ajouter(produitRequest);
            _transactionStockRepository.Ajouter(transactionStockDTO);
            
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Produit ajouté au stock avec succès" }
            );
            
            return response;
        }
        catch (Exception e)
        {
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
                );
            
            return response;
        }
    }
    
    public BaseResponse ListerLesProduits()
    {
        try
        {
            List<ProduitDTO> produits = _produitRepository.Lister();
            
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: produits
            );
            
            return response;
        }
        catch (Exception e)
        {
            BaseResponse response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );
            
            return response;
        }
    }
}