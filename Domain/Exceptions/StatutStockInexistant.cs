namespace ApiCube.Domain.Exceptions;

public class StatutStockInexistant : Exception
{
    public StatutStockInexistant() : base("statut_stock_inexistant")
    {
    }

    public StatutStockInexistant(string message) : base(message)
    {
    }

    public StatutStockInexistant(string message, Exception innerException) : base(message, innerException)
    {
    }
}