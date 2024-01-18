namespace ApiCube.Domain.Exceptions;

public class ProduitDejaDansPanier : Exception
{
    public ProduitDejaDansPanier() : base("produit_deja_dans_panier")
    {
    }

    public ProduitDejaDansPanier(string message) : base(message)
    {
    }

    public ProduitDejaDansPanier(string message, Exception innerException) : base(message, innerException)
    {
    }
}