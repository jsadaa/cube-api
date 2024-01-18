namespace ApiCube.Domain.Exceptions;

public class StatutFactureInvalide : Exception
{
    public StatutFactureInvalide() : base("statut_facture_invalide")
    {
    }

    public StatutFactureInvalide(string message) : base(message)
    {
    }

    public StatutFactureInvalide(string message, Exception inner) : base(message, inner)
    {
    }
}