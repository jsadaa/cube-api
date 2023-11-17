namespace ApiCube.Persistence.Exceptions;

public class ClientIntrouvable : Exception
{
    public ClientIntrouvable() : base("client_non_trouve")
    {
    }

    public ClientIntrouvable(string message) : base(message)
    {
    }

    public ClientIntrouvable(string message, Exception inner) : base(message, inner)
    {
    }
}