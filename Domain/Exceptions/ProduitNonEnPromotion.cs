namespace ApiCube.Domain.Exceptions;

public class ProduitNonEnPromotion : Exception
{
    public ProduitNonEnPromotion() : base("Le produit n'est pas en promotion")
    {
    }
    
    public ProduitNonEnPromotion(string message) : base(message)
    {
    }
    
    public ProduitNonEnPromotion(string message, Exception innerException) : base(message, innerException)
    {
    }
}