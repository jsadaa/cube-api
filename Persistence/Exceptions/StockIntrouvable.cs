namespace ApiCube.Persistence.Exceptions;

public class StockIntrouvable : Exception
{
    public StockIntrouvable() : base("stock_introuvable")
    {
    }

    public StockIntrouvable(string message) : base(message)
    {
    }

    public StockIntrouvable(string message, Exception innerException) : base(message, innerException)
    {
    }
}