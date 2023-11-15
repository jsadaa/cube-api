using ApiCube.Application.DTOs.Requests.Stock;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Stock;

namespace ApiCube.Domain.Services;

public class PreparateurDeStock
{
    public Stock Achat(Produit produit, StockRequest stockRequest, TypeTransactionStock typeTransactionStock)
    {
        var nouveauStock = new Stock(
            quantite: 0,
            seuilDisponibilite: stockRequest.SeuilDisponibilite,
            produit: produit,
            transactionStocks: new List<TransactionStock>(),
            dateCreation: DateTime.Now,
            datePeremption: stockRequest.DatePeremption,
            dateModification: DateTime.Now,
            dateSuppression: null
        );

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

        return nouveauStock;
    }

    public Stock ModificationInterne(Stock stock, StockUpdate stockUpdate)
    {
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

        return stock;
    }

    public Stock Suppression(Stock stock)
    {
        var nouvelleTransactionStock = new TransactionStock(
            quantite: -stock.Quantite,
            date: DateTime.Now,
            type: TypeTransactionStock.Suppression,
            stock: stock,
            prixUnitaire: stock.Produit.PrixAchat,
            quantiteAvant: stock.Quantite,
            quantiteApres: 0
        );

        stock.AjouterTransaction(nouvelleTransactionStock);

        return stock;
    }
}