using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Enums.Stock;

public enum StatutStock
{
    EnStock,
    EnRuptureDeStock,
    Indisponible,
    EnCommande,
    EnCoursDeLivraison,
    Livre,
    Perime,
    Retourne,
    Vendu,
    Perdu,
    Vole,
    Casse,
    Autre,
}

public class StatutStockMapper
{
    public StatutStock Mapper(string statut)
    {
        return statut switch
        {
            "EnStock" => StatutStock.EnStock,
            "EnRuptureDeStock" => StatutStock.EnRuptureDeStock,
            "Indisponible" => StatutStock.Indisponible,
            "EnCommande" => StatutStock.EnCommande,
            "EnCoursDeLivraison" => StatutStock.EnCoursDeLivraison,
            "Livre" => StatutStock.Livre,
            "Perime" => StatutStock.Perime,
            "Retourne" => StatutStock.Retourne,
            "Vendu" => StatutStock.Vendu,
            "Perdu" => StatutStock.Perdu,
            "Vole" => StatutStock.Vole,
            "Casse" => StatutStock.Casse,
            "Autre" => StatutStock.Autre,
            _ => throw new StatutStockInexistant()
        };
    }
}