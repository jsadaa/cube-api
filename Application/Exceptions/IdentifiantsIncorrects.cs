namespace ApiCube.Application.Exceptions;

public class IdentifiantsIncorrects : Exception
{
    public IdentifiantsIncorrects() : base("identifiants_incorrects")
    {
    }

    public IdentifiantsIncorrects(string message) : base(message)
    {
    }

    public IdentifiantsIncorrects(string message, Exception inner) : base(message, inner)
    {
    }
}