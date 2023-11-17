namespace ApiCube.Persistence.Exceptions;

public class ProduitIntrouvable : Exception
{
    public ProduitIntrouvable() : base("produit_introuvable")
    {
    }

    public ProduitIntrouvable(string message) : base(message)
    {
    }

    public ProduitIntrouvable(string message, Exception inner) : base(message, inner)
    {
    }
}