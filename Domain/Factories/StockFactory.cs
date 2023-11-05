using ApiCube.Application.DTOs.Requests;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Stock;
using ApiCube.Persistence.Models;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.TransactionStock;
using AutoMapper;

namespace ApiCube.Domain.Factories;

public class StockFactory
{
    private readonly ITransactionStockRepository _transactionStockRepository;
    private readonly IProduitRepository _produitRepository;
    private readonly ProduitFactory _produitFactory;
    private readonly TransactionStockFactory _transactionStockFactory;
    private readonly StatutStockMapper _statutStockMapper;
    private readonly IMapper _mapper;
    
    public StockFactory(ITransactionStockRepository transactionStockRepository, IProduitRepository produitRepository, ProduitFactory produitFactory, TransactionStockFactory transactionStockFactory, StatutStockMapper statutStockMapper, IMapper mapper)
    {
        _transactionStockRepository = transactionStockRepository;
        _produitRepository = produitRepository;
        _produitFactory = produitFactory;
        _transactionStockFactory = transactionStockFactory;
        _statutStockMapper = statutStockMapper;
        _mapper = mapper;
    }
    
    public Stock Creer(StockRequestDTO stockDTO)
    {
        Produit produit = _produitRepository.Trouver(stockDTO.ProduitId);
        List<TransactionStock> transactionsStock = new List<TransactionStock>();
        StatutStock statutStock = _statutStockMapper.Mapper(stockDTO.Statut);
        
        Stock stock = new Stock(
            stockDTO.Quantite, 
            stockDTO.SeuilDisponibilite, 
            statutStock,
            produit,
            transactionsStock,
            DateTime.Now, 
            stockDTO.DatePeremption, 
            DateTime.Now, 
            null
        );
        
        return stock;
    }
    
    public Stock Mapper(StockModel stockModel)
    {
        Produit produit = _produitFactory.Mapper(stockModel.Produit);
        List<TransactionStock> transactionsStock = _transactionStockRepository.ListerParStock(stockModel.Id);
        StatutStock statutStock = _statutStockMapper.Mapper(stockModel.Statut);
        
        Stock stock = new Stock(
            id: stockModel.Id,
            quantite: stockModel.Quantite, 
            seuilDisponibilite: stockModel.SeuilDisponibilite, 
            statut: statutStock,
            produit: produit,
            transactionStocks: transactionsStock,
            dateCreation: stockModel.DateCreation, 
            datePeremption: stockModel.DatePeremption, 
            dateModification: stockModel.DateModification, 
            dateSuppression: stockModel.DateSuppression
        );
        
        return stock;
    }
}