namespace ApiCube.Persistence.Exceptions;

public class LigneCommandeFournisseurIntrouvable : Exception
{
    public LigneCommandeFournisseurIntrouvable() : base("ligne_commande_fournisseur_introuvable")
    {
    }

    public LigneCommandeFournisseurIntrouvable(string message) : base(message)
    {
    }

    public LigneCommandeFournisseurIntrouvable(string message, Exception innerException) : base(message, innerException)
    {
    }
}