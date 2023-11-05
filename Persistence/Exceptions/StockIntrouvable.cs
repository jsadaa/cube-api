namespace ApiCube.Persistence.Exceptions;

public class StockIntrouvable : Exception
{
    public StockIntrouvable() : base("Le stock n'a pas été trouvé.")
    {
    }
    
    public StockIntrouvable(string message) : base(message)
    {
    }
    
    public StockIntrouvable(string message, Exception innerException) : base(message, innerException)
    {
    }
}