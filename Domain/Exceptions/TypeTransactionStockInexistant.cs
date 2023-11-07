namespace ApiCube.Domain.Exceptions;

public class TypeTransactionStockInexistant : Exception
{
    public TypeTransactionStockInexistant() : base("Le type de transaction n'existe pas")
    {
    }

    public TypeTransactionStockInexistant(string message) : base(message)
    {
    }

    public TypeTransactionStockInexistant(string message, Exception innerException) : base(message, innerException)
    {
    }
}