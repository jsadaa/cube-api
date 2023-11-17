namespace ApiCube.Domain.Exceptions;

public class TypeTransactionStockInexistant : Exception
{
    public TypeTransactionStockInexistant() : base("type_transaction_stock_inexistant")
    {
    }

    public TypeTransactionStockInexistant(string message) : base(message)
    {
    }

    public TypeTransactionStockInexistant(string message, Exception innerException) : base(message, innerException)
    {
    }
}