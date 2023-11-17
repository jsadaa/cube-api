namespace ApiCube.Application.Exceptions;

public class UtilisateurIntrouvable : Exception
{
    public UtilisateurIntrouvable() : base("utilisateur_introuvable")
    {
    }

    public UtilisateurIntrouvable(string message) : base(message)
    {
    }

    public UtilisateurIntrouvable(string message, Exception inner) : base(message, inner)
    {
    }
}