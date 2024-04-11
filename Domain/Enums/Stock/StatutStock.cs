using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Enums.Stock;

public enum StatutStock
{
    EnStock,
    EnRuptureDeStock,
    Indisponible,
    QuasimentEpuise,
    EnCommande,
    EnCoursDeLivraison,
    Perime,
    Retourne,
    Vendu,
    Perdu,
    Vole,
    Casse,
    Supprime,
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
            "QuasimentEpuise" => StatutStock.QuasimentEpuise,
            "Perime" => StatutStock.Perime,
            "Retourne" => StatutStock.Retourne,
            "Vendu" => StatutStock.Vendu,
            "Perdu" => StatutStock.Perdu,
            "Vole" => StatutStock.Vole,
            "Casse" => StatutStock.Casse,
            "Supprime" => StatutStock.Supprime,
            _ => throw new StatutStockInexistant()
        };
    }
}