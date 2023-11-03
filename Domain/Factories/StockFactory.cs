using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Stock;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.TransactionStock;

namespace ApiCube.Domain.Factories;

public class StockFactory
{
    private readonly ITransactionStockRepository _transactionStockRepository;
    private readonly IProduitRepository _produitRepository;
    private readonly ProduitFactory _produitFactory;
    private readonly TransactionStockFactory _transactionStockFactory;
    private readonly StatutStockMapper _statutStockMapper;
    
    public StockFactory(ITransactionStockRepository transactionStockRepository, IProduitRepository produitRepository, ProduitFactory produitFactory, TransactionStockFactory transactionStockFactory, StatutStockMapper statutStockMapper)
    {
        _transactionStockRepository = transactionStockRepository;
        _produitRepository = produitRepository;
        _produitFactory = produitFactory;
        _transactionStockFactory = transactionStockFactory;
        _statutStockMapper = statutStockMapper;
    }
    
    public Stock CréerStock(AjouterStockRequest stockDTO)
    {
        ProduitDTO? produitDTO = _produitRepository.Trouver(stockDTO.ProduitId);

        if (produitDTO == null) throw new Exception("Le produit n'existe pas");
              
        Produit produit = _produitFactory.MapperProduit(produitDTO);
        List<TransactionStock> transactionsStock = new List<TransactionStock>();
        
        Stock stock = new Stock(
            stockDTO.Quantite, 
            stockDTO.SeuilDisponibilite, 
            StatutStock.EnStock, 
            produit,
            transactionsStock,
            DateTime.Now, 
            stockDTO.DatePeremption, 
            DateTime.Now, 
            null
        );
        
        return stock;
    }
    
    public Stock MapperStock(StockDTO stockDTO)
    {
        ProduitDTO? produitDTO = _produitRepository.Trouver(stockDTO.Produit.Id);

        if (produitDTO == null) throw new Exception("Le produit n'existe pas");
              
        Produit produit = _produitFactory.MapperProduit(produitDTO);
        List<TransactionStockDTO> transactionsStockDTO = _transactionStockRepository.ListerParStock(stockDTO.Id);
        List<TransactionStock> transactionsStock = new List<TransactionStock>();
        
        Stock stock = new Stock(
            stockDTO.Id,
            stockDTO.Quantite, 
            stockDTO.SeuilDisponibilite, 
            _statutStockMapper.Mapper(stockDTO.Statut),
            produit,
            transactionsStock,
            stockDTO.DateCreation, 
            stockDTO.DatePeremption, 
            stockDTO.DateModification, 
            stockDTO.DateSuppression
        );
        
        transactionsStock = transactionsStockDTO.Select(
            transactionStockDTO => _transactionStockFactory.MapperTransactionStock(
                stock, 
                transactionStockDTO
            )
        ).ToList();
        
        stock.Transactions = transactionsStock;
        
        return stock;
    }
}