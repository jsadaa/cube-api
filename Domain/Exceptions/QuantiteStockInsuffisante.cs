namespace ApiCube.Domain.Exceptions;

public class QuantiteStockInsuffisante : Exception
{
    public QuantiteStockInsuffisante() : base("quantite_stock_insuffisante")
    {
    }

    public QuantiteStockInsuffisante(string message) : base(message)
    {
    }

    public QuantiteStockInsuffisante(string message, Exception inner) : base(message, inner)
    {
    }
}