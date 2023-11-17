namespace ApiCube.Application.Exceptions;

public class RefreshTokenInvalide : Exception
{
    public RefreshTokenInvalide() : base("Le token de rafraîchissement est invalide.")
    {
    }

    public RefreshTokenInvalide(string message) : base(message)
    {
    }

    public RefreshTokenInvalide(string message, Exception inner) : base(message, inner)
    {
    }
}