using ApiCube.Application.DTOs.Requests.Stock;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Stock;

namespace ApiCube.Domain.Services;

public class PreparateurDeStock
{
    public Stock AjoutInterne(Produit produit, StockRequest stockRequest)
    {
        var nouveauStock = new Stock(
            0,
            stockRequest.SeuilDisponibilite,
            produit,
            new List<TransactionStock>(),
            DateTime.Now,
            DateTime.Now,
            null
        );

        var nouvelleTransactionStock = new TransactionStock(
            stockRequest.Quantite,
            DateTime.Now,
            TypeTransactionStock.AjoutInterne,
            nouveauStock,
            produit.PrixAchat,
            0,
            stockRequest.Quantite
        );

        nouveauStock.AjouterTransaction(nouvelleTransactionStock);

        return nouveauStock;
    }

    public Stock Approvisionnement(Stock stock, LigneCommandeFournisseur ligneCommandeFournisseur)
    {
        var nouvelleTransactionStock = new TransactionStock(
            ligneCommandeFournisseur.Quantite,
            DateTime.Now,
            TypeTransactionStock.Approvisionnement,
            stock,
            ligneCommandeFournisseur.PrixUnitaire,
            stock.Quantite,
            stock.Quantite + ligneCommandeFournisseur.Quantite
        );

        stock.AjouterTransaction(nouvelleTransactionStock);

        return stock;
    }

    public Stock ModificationInterne(Stock stock, StockUpdate stockUpdate)
    {
        if (stock.Quantite != stockUpdate.Quantite)
        {
            var nouvelleTransactionStock = new TransactionStock(
                stockUpdate.Quantite - stock.Quantite,
                DateTime.Now,
                TypeTransactionStock.ModificationInterne,
                stock,
                stock.Produit.PrixAchat,
                stock.Quantite,
                stockUpdate.Quantite
            );

            stock.AjouterTransaction(nouvelleTransactionStock);
        }

        stock.ModifierSeuilDisponibilite(stockUpdate.SeuilDisponibilite);

        return stock;
    }

    public Stock Suppression(Stock stock)
    {
        var nouvelleTransactionStock = new TransactionStock(
            -stock.Quantite,
            DateTime.Now,
            TypeTransactionStock.Suppression,
            stock,
            stock.Produit.PrixAchat,
            stock.Quantite,
            0
        );

        stock.AjouterTransaction(nouvelleTransactionStock);

        return stock;
    }
}