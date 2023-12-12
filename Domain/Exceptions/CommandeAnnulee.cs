namespace ApiCube.Domain.Exceptions;

public class CommandeAnnulee : Exception
{
    public CommandeAnnulee() : base("commande_annulee")
    {
    }

    public CommandeAnnulee(string message) : base(message)
    {
    }

    public CommandeAnnulee(string message, Exception innerException) : base(message, innerException)
    {
    }
}