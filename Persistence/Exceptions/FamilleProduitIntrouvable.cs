namespace ApiCube.Persistence.Exceptions;

public class FamilleProduitIntrouvable : Exception
{
    public FamilleProduitIntrouvable() : base("famille_produit_introuvable")
    {
    }

    public FamilleProduitIntrouvable(string message) : base(message)
    {
    }

    public FamilleProduitIntrouvable(string message, Exception innerException) : base(message, innerException)
    {
    }
}