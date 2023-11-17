namespace ApiCube.Application.Exceptions;

public class RefreshTokenInvalide : Exception
{
    public RefreshTokenInvalide() : base("refresh_token_invalide")
    {
    }

    public RefreshTokenInvalide(string message) : base(message)
    {
    }

    public RefreshTokenInvalide(string message, Exception inner) : base(message, inner)
    {
    }
}