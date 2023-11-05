namespace ApiCube.Domain.Exceptions;

public class StatutStockInexistant : Exception
{
    public StatutStockInexistant() : base("Le statut de stock n'existe pas")
    {
    }
    
    public StatutStockInexistant(string message) : base(message)
    {
    }
    
    public StatutStockInexistant(string message, Exception innerException) : base(message, innerException)
    {
    }
}