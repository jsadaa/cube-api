namespace ApiCube.Persistence.Exceptions;

public class CommandeClientIntrouvable : Exception
{
    public CommandeClientIntrouvable() : base("commande_client_introuvable")
    {
    }

    public CommandeClientIntrouvable(string message) : base(message)
    {
    }

    public CommandeClientIntrouvable(string message, Exception inner) : base(message, inner)
    {
    }
}