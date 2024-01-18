namespace ApiCube.Persistence.Exceptions;

public class PanierClientIntrouvable : Exception
{
    public PanierClientIntrouvable() : base("panier_client_introuvable")
    {
    }

    public PanierClientIntrouvable(string message) : base(message)
    {
    }

    public PanierClientIntrouvable(string message, Exception innerException) : base(message, innerException)
    {
    }
}