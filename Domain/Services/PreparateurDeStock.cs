using ApiCube.Application.DTOs.Requests.Stock;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Stock;

namespace ApiCube.Domain.Services;

public class PreparateurDeStock
{
    public Stock AjoutInterne(Produit produit, StockRequest stockRequest)
    {
        var nouveauStock = new Stock(
            quantite: 0,
            seuilDisponibilite: stockRequest.SeuilDisponibilite,
            produit: produit,
            transactionStocks: new List<TransactionStock>(),
            dateCreation: DateTime.Now,
            dateModification: DateTime.Now,
            dateSuppression: null
        );

        var nouvelleTransactionStock = new TransactionStock(
            quantite: stockRequest.Quantite,
            date: DateTime.Now,
            type: TypeTransactionStock.AjoutInterne,
            stock: nouveauStock,
            prixUnitaire: produit.PrixAchat,
            quantiteAvant: 0,
            quantiteApres: stockRequest.Quantite
        );

        nouveauStock.AjouterTransaction(nouvelleTransactionStock);

        return nouveauStock;
    }
    
    public Stock Achat(Stock stock, LigneCommandeFournisseur ligneCommandeFournisseur)
    {
        var nouvelleTransactionStock = new TransactionStock(
            quantite: ligneCommandeFournisseur.Quantite,
            date: DateTime.Now,
            type: TypeTransactionStock.Achat,
            stock: stock,
            prixUnitaire: ligneCommandeFournisseur.PrixUnitaire,
            quantiteAvant: stock.Quantite,
            quantiteApres: stock.Quantite + ligneCommandeFournisseur.Quantite
        );

        stock.AjouterTransaction(nouvelleTransactionStock);

        return stock;
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