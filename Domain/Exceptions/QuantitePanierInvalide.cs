namespace ApiCube.Domain.Exceptions;

public class QuantitePanierInvalide : Exception
{
    public QuantitePanierInvalide() : base("quantite_panier_invalide")
    {
    }

    public QuantitePanierInvalide(string message) : base(message)
    {
    }

    public QuantitePanierInvalide(string message, Exception innerException) : base(message, innerException)
    {
    }
}