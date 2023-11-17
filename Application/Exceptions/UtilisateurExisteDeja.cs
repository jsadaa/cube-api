namespace ApiCube.Application.Exceptions;

public class UtilisateurExisteDeja : Exception
{
    public UtilisateurExisteDeja() : base("Un utilisateur avec ce nom ou cet email existe déjà")
    {
    }

    public UtilisateurExisteDeja(string message) : base(message)
    {
    }

    public UtilisateurExisteDeja(string message, Exception innerException) : base(message, innerException)
    {
    }
}