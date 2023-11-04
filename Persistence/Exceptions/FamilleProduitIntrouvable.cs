namespace ApiCube.Persistence.Exceptions;

public class FamilleProduitIntrouvable : Exception
{
    public FamilleProduitIntrouvable() : base("La famille de produit n'a pas été trouvée.")
    {
    }
    
    public FamilleProduitIntrouvable(string message) : base(message)
    {
    }
    
    public FamilleProduitIntrouvable(string message, Exception innerException) : base(message, innerException)
    {
    }
}