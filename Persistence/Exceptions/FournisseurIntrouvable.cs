namespace ApiCube.Persistence.Exceptions;

public class FournisseurIntrouvable : Exception
{
    public FournisseurIntrouvable() : base("fournisseur_introuvable")
    {
    }

    public FournisseurIntrouvable(string message) : base(message)
    {
    }

    public FournisseurIntrouvable(string message, Exception innerException) : base(message, innerException)
    {
    }
}