namespace ApiCube.Domain.Exceptions;

public class ProduitNonPresentDansPanier : Exception
{
    public ProduitNonPresentDansPanier() : base("produit_non_present_dans_panier")
    {
    }

    public ProduitNonPresentDansPanier(string message) : base(message)
    {
    }

    public ProduitNonPresentDansPanier(string message, Exception innerException) : base(message, innerException)
    {
    }
}