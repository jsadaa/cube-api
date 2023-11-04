using ApiCube.Domain.Entities;

namespace ApiCube.Persistence.Exceptions;

public class FournisseurIntrouvable : Exception
{
    public FournisseurIntrouvable() : base("Le fournisseur n'a pas été trouvé.")
    {
    }
    
    public FournisseurIntrouvable(string message) : base(message)
    {
    }
    
    public FournisseurIntrouvable(string message, Exception innerException) : base(message, innerException)
    {
    }
}