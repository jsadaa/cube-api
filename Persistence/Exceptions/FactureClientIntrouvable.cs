namespace ApiCube.Persistence.Exceptions;

public class FactureClientIntrouvable : Exception
{
    public FactureClientIntrouvable() : base("facture_client_introuvable")
    {
    }

    public FactureClientIntrouvable(string message) : base(message)
    {
    }

    public FactureClientIntrouvable(string message, Exception innerException) : base(message, innerException)
    {
    }
}