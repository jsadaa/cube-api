namespace ApiCube.Application.Exceptions;

public class FormatMotDePasseInvalide : Exception
{
    public FormatMotDePasseInvalide() : base(
        "Le mot de passe doit contenir au moins 8 caractères dont une majuscule, une minuscule, un chiffre et un caractère spécial")
    {
    }

    public FormatMotDePasseInvalide(string message) : base(message)
    {
    }

    public FormatMotDePasseInvalide(string message, Exception innerException) : base(message, innerException)
    {
    }
}