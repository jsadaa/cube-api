using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Enums.Facture;

public enum StatutFacture
{
    EnCours,
    Payee,
    Annulee,
    Autre
}

public class StatutFactureMapper
{
    public StatutFacture Mapper(string statut)
    {
        return statut switch
        {
            "EnCours" => StatutFacture.EnCours,
            "Payee" => StatutFacture.Payee,
            "Annulee" => StatutFacture.Annulee,
            "Autre" => StatutFacture.Autre,
            _ => throw new StatutFactureInvalide()
        };
    }
}