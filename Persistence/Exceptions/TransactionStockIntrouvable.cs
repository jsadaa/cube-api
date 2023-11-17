namespace ApiCube.Persistence.Exceptions;

public class TransactionStockIntrouvable : Exception
{
    public TransactionStockIntrouvable() : base("transaction_stock_introuvable")
    {
    }

    public TransactionStockIntrouvable(string message) : base(message)
    {
    }

    public TransactionStockIntrouvable(string message, Exception inner) : base(message, inner)
    {
    }
}