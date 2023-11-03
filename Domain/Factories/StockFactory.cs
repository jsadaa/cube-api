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
    
    public StockFactory(ITransactionStockRepository transactionStockRepository, IProduitRepository produitRepository, ProduitFactory produitFactory, TransactionStockFactory transactionStockFactory)
    {
        _transactionStockRepository = transactionStockRepository;
        _produitRepository = produitRepository;
        _produitFactory = produitFactory;
        _transactionStockFactory = transactionStockFactory;
    }
    
    private Stock CréerStock(AjouterStockRequest stock)
    {
        ProduitDTO? produitDTO = _produitRepository.Trouver(stock.ProduitId);
        List<TransactionStockDTO> transactionsStockDTO = _transactionStockRepository.ListerParStock(stock.ProduitId);

        if (produitDTO == null) throw new Exception("Le produit n'existe pas");
              
        Produit produit = _produitFactory.MapperProduit(produitDTO);
        List<TransactionStock> transactionsStock = new List<TransactionStock>();

        foreach (var transactionStock in transactionsStockDTO)
        {
            transactionsStock.Add(_transactionStockFactory.MapperTransactionStock(produit, transactionStock));
        }
        
        Stock stockEntity = new Stock(
            stock.Quantite, 
            stock.SeuilDisponibilite, 
            StatutStock.EnStock, 
            produit,
            transactionsStock,
            DateTime.Now, 
            stock.DatePeremption, 
            DateTime.Now, 
            null
        );
        
        return stockEntity;
    }
    
}