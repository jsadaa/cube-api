namespace ApiCube.Application.Exceptions;

public class EmailOuMotDePasseIncorrect : Exception
{
    public EmailOuMotDePasseIncorrect() : base("L'email ou le mot de passe est incorrect.")
    {
    }

    public EmailOuMotDePasseIncorrect(string message) : base(message)
    {
    }

    public EmailOuMotDePasseIncorrect(string message, Exception inner) : base(message, inner)
    {
    }
}