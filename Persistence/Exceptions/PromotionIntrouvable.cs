namespace ApiCube.Persistence.Exceptions;

public class PromotionIntrouvable : Exception
{
    public PromotionIntrouvable() : base("La promotion est introuvable")
    {
    }

    public PromotionIntrouvable(string message) : base(message)
    {
    }

    public PromotionIntrouvable(string message, Exception innerException) : base(message, innerException)
    {
    }
}