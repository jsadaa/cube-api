namespace ApiCube.Domain.Exceptions;

public class AdresseNonValide : Exception
{
    public AdresseNonValide() : base("L'adresse doit être composée de 5 champs séparés par une virgule")
    {
    }
    public AdresseNonValide(string message) : base(message)
    {
    }
    
    public AdresseNonValide(string message, Exception innerException) : base(message, innerException)
    {
    }
}