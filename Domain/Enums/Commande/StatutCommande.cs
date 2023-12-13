using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Enums.Commande;

public enum StatutCommande
{
    EnCours,
    Livree,
    Annulee,
    Autre
}

public class StatutCommandeMapper
{
    public StatutCommande Mapper(string statut)
    {
        return statut switch
        {
            "EnCours" => StatutCommande.EnCours,
            "Livree" => StatutCommande.Livree,
            "Annulee" => StatutCommande.Annulee,
            "Autre" => StatutCommande.Autre,
            _ => throw new StatutCommandeInvalide()
        };
    }
}