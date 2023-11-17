namespace ApiCube.Application.Exceptions;

public class UtilisateurExisteDeja : Exception
{
    public UtilisateurExisteDeja() : base("utilisateur_existe_deja")
    {
    }

    public UtilisateurExisteDeja(string message) : base(message)
    {
    }

    public UtilisateurExisteDeja(string message, Exception innerException) : base(message, innerException)
    {
    }
}