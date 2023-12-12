namespace ApiCube.Domain.Exceptions;

public class StatutCommandeInvalide : Exception
{
    public StatutCommandeInvalide() : base("statut_commande_invalide")
    {
    }

    public StatutCommandeInvalide(string message) : base(message)
    {
    }

    public StatutCommandeInvalide(string message, Exception innerException) : base(message, innerException)
    {
    }
}