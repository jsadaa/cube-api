namespace ApiCube.Domain.Exceptions;

public class PanierDejaExistant : Exception
{
    public PanierDejaExistant() : base("panier_deja_existant")
    {
    }

    public PanierDejaExistant(string message) : base(message)
    {
    }

    public PanierDejaExistant(string message, Exception innerException) : base(message, innerException)
    {
    }
}
