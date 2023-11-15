using ApiCube.Persistence.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Seeders;

public static class StockSeeder
{
    public static void SeedStocksAndTransactions(ApiDbContext context, List<ProduitModel> produits)
    {
        if (context.Stocks.Any()) return;

        foreach (var produit in produits)
        {
            // Créer un stock par produit
            var stock = new StockModel
            {
                ProduitId = produit.Id,
                Produit = produit,
                Quantite = new Faker().Random.Number(10, 100),
                SeuilDisponibilite = new Faker().Random.Number(2, 5),
                Statut = "EnStock",
                DateCreation = new Faker().Date.Past(),
                DateModification = new Faker().Date.Recent(),
            };
            context.Stocks.Add(stock);
        }

        // Sauvegarder les stocks pour obtenir leur ID
        context.SaveChanges();

        foreach (var stock in context.Stocks.Include(stockModel => stockModel.Produit).ToList())
        {
            // Créer une transaction pour chaque création de stock
            var transaction = new TransactionStockModel
            {
                StockId = stock.Id,
                Stock = stock,
                Quantite = stock.Quantite,
                Date = DateTime.Now,
                Type = "Achat",
                PrixUnitaire = stock.Produit.PrixAchat,
                PrixTotal = stock.Quantite * stock.Produit.PrixAchat,
                QuantiteAvant = 0,
                QuantiteApres = stock.Quantite
            };
            context.TransactionsStock.Add(transaction);
        }

        context.SaveChanges();
    }
}