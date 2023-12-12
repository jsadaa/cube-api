namespace ApiCube.Domain.Exceptions;

public class DateCommandeInvalide : Exception
{
    public DateCommandeInvalide() : base("date_commande_invalide")
    {
    }

    public DateCommandeInvalide(string message) : base(message)
    {
    }

    public DateCommandeInvalide(string message, Exception innerException) : base(message, innerException)
    {
    }
}