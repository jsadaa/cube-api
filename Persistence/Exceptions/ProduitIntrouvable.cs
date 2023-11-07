namespace ApiCube.Persistence.Exceptions;

public class ProduitIntrouvable : Exception
{
    public ProduitIntrouvable() : base("Le produit n'a pas été trouvé")
    {
    }

    public ProduitIntrouvable(string message) : base(message)
    {
    }

    public ProduitIntrouvable(string message, Exception inner) : base(message, inner)
    {
    }
}