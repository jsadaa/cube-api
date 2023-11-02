using ApiCube.Domain.Entities;
using ApiCube.Domain.Stock;

namespace ApiCube.Domain.Factories;

public class TransactionStockFactory
{
    public TransactionStock CreerTransactionStock(Produit produit, TypeTransactionStock type)
    {
        return new TransactionStock(
            quantite: produit.Quantite,
            date: DateTime.Now,
            type: type,
            produit: produit,
            prixUnitaire: produit.PrixAchat
        );
    }

}