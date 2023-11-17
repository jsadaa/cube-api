namespace ApiCube.Application.Exceptions;

public class FormatMotDePasseInvalide : Exception
{
    public FormatMotDePasseInvalide() : base(
        "format_mot_de_passe_invalide")
    {
    }

    public FormatMotDePasseInvalide(string message) : base(message)
    {
    }

    public FormatMotDePasseInvalide(string message, Exception innerException) : base(message, innerException)
    {
    }
}