namespace ApiCube.Persistence.Exceptions;

public class CommandeFournisseurIntrouvable : Exception
{
    public CommandeFournisseurIntrouvable() : base("commande_fournisseur_introuvable")
    {
    }

    public CommandeFournisseurIntrouvable(string message) : base(message)
    {
    }

    public CommandeFournisseurIntrouvable(string message, Exception innerException) : base(message, innerException)
    {
    }
}