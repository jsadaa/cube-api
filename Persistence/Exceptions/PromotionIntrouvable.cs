namespace ApiCube.Persistence.Exceptions;

public class PromotionIntrouvable : Exception
{
    public PromotionIntrouvable() : base("promotion_introuvable")
    {
    }

    public PromotionIntrouvable(string message) : base(message)
    {
    }

    public PromotionIntrouvable(string message, Exception innerException) : base(message, innerException)
    {
    }
}