namespace ApiCube.Application.Exceptions;

public class ClaimInvalide : Exception
{
    public ClaimInvalide() : base("claim_invalide")
    {
    }

    public ClaimInvalide(string message) : base(message)
    {
    }

    public ClaimInvalide(string message, Exception inner) : base(message, inner)
    {
    }
}