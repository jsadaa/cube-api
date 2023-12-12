namespace ApiCube.Domain.Exceptions;

public class StatutCommandeInexistant : Exception
{
    public StatutCommandeInexistant() : base("statut_commande_inexistant")
    {
    }

    public StatutCommandeInexistant(string message) : base(message)
    {
    }

    public StatutCommandeInexistant(string message, Exception innerException) : base(message, innerException)
    {
    }
}