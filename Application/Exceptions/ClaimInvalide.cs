namespace ApiCube.Application.Exceptions;

public class ClaimInvalide : Exception
{
    public ClaimInvalide() : base("Le claim est invalide.")
    {
    }

    public ClaimInvalide(string message) : base(message)
    {
    }

    public ClaimInvalide(string message, Exception inner) : base(message, inner)
    {
    }
}