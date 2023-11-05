namespace ApiCube.Domain.Exceptions;

public class NumeroTelephoneNonValide : Exception
{
    public NumeroTelephoneNonValide() : base("Le numéro de téléphone doit être composé de 10 chiffres")
    {
    }
    
    public NumeroTelephoneNonValide(string message) : base(message)
    {
    }
    
    public NumeroTelephoneNonValide(string message, Exception innerException) : base(message, innerException)
    {
    }
}