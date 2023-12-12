namespace ApiCube.Domain.Exceptions;

public class CommandeDejaLivree : Exception
{
    public CommandeDejaLivree() : base("commande_deja_livree")
    {
    }

    public CommandeDejaLivree(string message) : base(message)
    {
    }

    public CommandeDejaLivree(string message, Exception innerException) : base(message, innerException)
    {
    }
}