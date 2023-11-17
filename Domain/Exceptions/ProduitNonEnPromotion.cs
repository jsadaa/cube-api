namespace ApiCube.Domain.Exceptions;

public class ProduitNonEnPromotion : Exception
{
    public ProduitNonEnPromotion() : base("produit_non_en_promotion")
    {
    }

    public ProduitNonEnPromotion(string message) : base(message)
    {
    }

    public ProduitNonEnPromotion(string message, Exception innerException) : base(message, innerException)
    {
    }
}